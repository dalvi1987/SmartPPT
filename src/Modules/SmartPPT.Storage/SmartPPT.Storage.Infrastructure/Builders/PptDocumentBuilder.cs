using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using A = DocumentFormat.OpenXml.Drawing;
using P = DocumentFormat.OpenXml.Presentation;
using Microsoft.Extensions.Options;
using SmartPPT.SlideEngine.Contracts.Enums;
using SmartPPT.SlideEngine.Contracts.Layout;
using SmartPPT.Storage.Application.Builders;
using SmartPPT.Storage.Domain.Files;
using SmartPPT.Storage.Infrastructure.Configuration;

namespace SmartPPT.Storage.Infrastructure.Builders;

public sealed class PptDocumentBuilder : IDocumentBuilder
{
    private const long SlideWidth = 9144000L;
    private const long SlideHeight = 6858000L;
    private const long DefaultTitleX = 457200L;
    private const long DefaultTitleY = 274320L;
    private const long DefaultTitleWidth = 8229600L;
    private const long DefaultTitleHeight = 822960L;
    private const long DefaultContentX = 640080L;
    private const long DefaultContentY = 1371600L;
    private const long DefaultContentWidth = 7863840L;
    private const long DefaultContentHeight = 4572000L;

    private readonly StorageOptions _options;

    public PptDocumentBuilder(IOptions<StorageOptions> options)
    {
        _options = options.Value;
    }

    public StoredFile BuildDocument(List<RenderableSlideDto> slides)
    {
        ArgumentNullException.ThrowIfNull(slides);

        Directory.CreateDirectory(_options.StoragePath);

        var fileName = $"{Guid.NewGuid()}.pptx";
        var filePath = Path.Combine(_options.StoragePath, fileName);
        var createdAt = DateTime.UtcNow;

        using (var document = PresentationDocument.Create(filePath, PresentationDocumentType.Presentation))
        {
            var presentationPart = document.AddPresentationPart();
            presentationPart.Presentation = new P.Presentation();

            var slideMasterPart = CreateSlideMasterPart(presentationPart);
            var slideLayoutPart = slideMasterPart.SlideLayoutParts.First();
            var slideIdList = new P.SlideIdList();
            uint slideId = 256U;

            foreach (var slide in slides)
            {
                var slidePart = presentationPart.AddNewPart<SlidePart>();
                CreateSlide(slidePart, slide, slideLayoutPart);

                slideIdList.Append(new P.SlideId
                {
                    Id = slideId++,
                    RelationshipId = presentationPart.GetIdOfPart(slidePart)
                });
            }

            presentationPart.Presentation.Append(new P.SlideMasterIdList(
                new P.SlideMasterId
                {
                    Id = 2147483648U,
                    RelationshipId = presentationPart.GetIdOfPart(slideMasterPart)
                }));

            presentationPart.Presentation.Append(slideIdList);
            presentationPart.Presentation.SlideSize = new P.SlideSize { Cx = (int)SlideWidth, Cy = (int)SlideHeight };
            presentationPart.Presentation.NotesSize = new P.NotesSize { Cx = (int)SlideHeight, Cy = (int)SlideWidth };
            presentationPart.Presentation.Save();
        }

        var fileInfo = new FileInfo(filePath);

        return new StoredFile
        {
            FileName = fileName,
            Path = filePath,
            CreatedAt = createdAt,
            Size = fileInfo.Exists ? fileInfo.Length : 0
        };
    }

    private static SlideMasterPart CreateSlideMasterPart(PresentationPart presentationPart)
    {
        var slideMasterPart = presentationPart.AddNewPart<SlideMasterPart>();
        var themePart = slideMasterPart.AddNewPart<ThemePart>();
        themePart.Theme = CreateTheme();

        var slideLayoutPart = slideMasterPart.AddNewPart<SlideLayoutPart>();
        slideLayoutPart.SlideLayout = new P.SlideLayout(
            new P.CommonSlideData(
                new P.ShapeTree(
                    new P.NonVisualGroupShapeProperties(
                        new P.NonVisualDrawingProperties { Id = 1U, Name = string.Empty },
                        new P.NonVisualGroupShapeDrawingProperties(),
                        new P.ApplicationNonVisualDrawingProperties()),
                    new P.GroupShapeProperties(new A.TransformGroup()))),
            new P.ColorMapOverride(new A.MasterColorMapping()))
        {
            Type = P.SlideLayoutValues.Text,
            Preserve = true
        };
        slideLayoutPart.SlideLayout.Save();

        slideMasterPart.SlideMaster = new P.SlideMaster(
            new P.CommonSlideData(
                new P.ShapeTree(
                    new P.NonVisualGroupShapeProperties(
                        new P.NonVisualDrawingProperties { Id = 1U, Name = string.Empty },
                        new P.NonVisualGroupShapeDrawingProperties(),
                        new P.ApplicationNonVisualDrawingProperties()),
                    new P.GroupShapeProperties(new A.TransformGroup()))),
            new P.ColorMap
            {
                Background1 = A.ColorSchemeIndexValues.Light1,
                Text1 = A.ColorSchemeIndexValues.Dark1,
                Background2 = A.ColorSchemeIndexValues.Light2,
                Text2 = A.ColorSchemeIndexValues.Dark2,
                Accent1 = A.ColorSchemeIndexValues.Accent1,
                Accent2 = A.ColorSchemeIndexValues.Accent2,
                Accent3 = A.ColorSchemeIndexValues.Accent3,
                Accent4 = A.ColorSchemeIndexValues.Accent4,
                Accent5 = A.ColorSchemeIndexValues.Accent5,
                Accent6 = A.ColorSchemeIndexValues.Accent6,
                Hyperlink = A.ColorSchemeIndexValues.Hyperlink,
                FollowedHyperlink = A.ColorSchemeIndexValues.FollowedHyperlink
            },
            new P.SlideLayoutIdList(
                new P.SlideLayoutId
                {
                    Id = 1U,
                    RelationshipId = slideMasterPart.GetIdOfPart(slideLayoutPart)
                }),
            new P.TextStyles(
                new P.TitleStyle(),
                new P.BodyStyle(),
                new P.OtherStyle()));
        slideMasterPart.SlideMaster.Save();

        return slideMasterPart;
    }

    private static void CreateSlide(SlidePart slidePart, RenderableSlideDto slide, SlideLayoutPart slideLayoutPart)
    {
        slidePart.AddPart(slideLayoutPart);

        var shapeTree = new P.ShapeTree(
            new P.NonVisualGroupShapeProperties(
                new P.NonVisualDrawingProperties { Id = 1U, Name = string.Empty },
                new P.NonVisualGroupShapeDrawingProperties(),
                new P.ApplicationNonVisualDrawingProperties()),
            new P.GroupShapeProperties(new A.TransformGroup()));

        shapeTree.Append(CreateTitleShape(slide.Title));
        shapeTree.Append(CreateBodyShape(slide.PositionedElements));

        slidePart.Slide = new P.Slide(
            new P.CommonSlideData(shapeTree),
            new P.ColorMapOverride(new A.MasterColorMapping()));
        slidePart.Slide.Save();
    }

    private static P.Shape CreateTitleShape(string title)
    {
        return CreateTextShape(
            id: 2U,
            name: "Title",
            textValue: string.IsNullOrWhiteSpace(title) ? "Untitled Slide" : title,
            x: DefaultTitleX,
            y: DefaultTitleY,
            width: DefaultTitleWidth,
            height: DefaultTitleHeight,
            fontSize: 2400,
            isBullet: false,
            placeholderType: P.PlaceholderValues.Title);
    }

    private static P.Shape CreateBodyShape(IEnumerable<PositionedElementDto> elements)
    {
        var textBody = new P.TextBody(
            new A.BodyProperties(),
            new A.ListStyle());

        var textElements = elements
            .Where(element => element.ElementType == ElementType.Text && !string.IsNullOrWhiteSpace(element.Content))
            .ToList();

        if (textElements.Count == 0)
        {
            textBody.Append(CreateParagraph(" ", isBullet: false, fontSize: 1800));
        }
        else
        {
            foreach (var element in textElements)
            {
                textBody.Append(CreateParagraph(element.Content, isBullet: true, fontSize: 1800));
            }
        }

        return new P.Shape(
            new P.NonVisualShapeProperties(
                new P.NonVisualDrawingProperties { Id = 3U, Name = "Content" },
                new P.NonVisualShapeDrawingProperties(new A.ShapeLocks { NoGrouping = true }),
                new P.ApplicationNonVisualDrawingProperties(
                    new P.PlaceholderShape { Type = P.PlaceholderValues.Body })),
            new P.ShapeProperties(
                new A.Transform2D(
                    new A.Offset { X = DefaultContentX, Y = DefaultContentY },
                    new A.Extents { Cx = DefaultContentWidth, Cy = DefaultContentHeight })),
            textBody);
    }

    private static P.Shape CreateTextShape(
        uint id,
        string name,
        string textValue,
        long x,
        long y,
        long width,
        long height,
        int fontSize,
        bool isBullet,
        P.PlaceholderValues placeholderType)
    {
        return new P.Shape(
            new P.NonVisualShapeProperties(
                new P.NonVisualDrawingProperties { Id = id, Name = name },
                new P.NonVisualShapeDrawingProperties(new A.ShapeLocks { NoGrouping = true }),
                new P.ApplicationNonVisualDrawingProperties(
                    new P.PlaceholderShape { Type = placeholderType })),
            new P.ShapeProperties(
                new A.Transform2D(
                    new A.Offset { X = x, Y = y },
                    new A.Extents { Cx = width, Cy = height })),
            new P.TextBody(
                new A.BodyProperties(),
                new A.ListStyle(),
                CreateParagraph(textValue, isBullet, fontSize)));
    }

    private static A.Paragraph CreateParagraph(string text, bool isBullet, int fontSize)
    {
        var paragraphProperties = new A.ParagraphProperties();

        if (isBullet)
        {
            paragraphProperties.Level = 0;
            paragraphProperties.LeftMargin = 342900;
            paragraphProperties.Indent = -171450;
            paragraphProperties.Append(new A.BulletFont { Typeface = "Arial" });
            paragraphProperties.Append(new A.CharacterBullet { Char = "•" });
        }

        return new A.Paragraph(
            paragraphProperties,
            new A.Run(
                new A.RunProperties { Language = "en-US", FontSize = fontSize, Dirty = false },
                new A.Text(text)),
            new A.EndParagraphRunProperties { Language = "en-US", FontSize = fontSize });
    }

    private static A.Theme CreateTheme()
    {
        return new A.Theme(
            new A.ThemeElements(
                new A.ColorScheme(
                    new A.Dark1Color(new A.SystemColor { Val = A.SystemColorValues.WindowText, LastColor = "000000" }),
                    new A.Light1Color(new A.SystemColor { Val = A.SystemColorValues.Window, LastColor = "FFFFFF" }),
                    new A.Dark2Color(new A.RgbColorModelHex { Val = "1F497D" }),
                    new A.Light2Color(new A.RgbColorModelHex { Val = "EEECE1" }),
                    new A.Accent1Color(new A.RgbColorModelHex { Val = "4F81BD" }),
                    new A.Accent2Color(new A.RgbColorModelHex { Val = "C0504D" }),
                    new A.Accent3Color(new A.RgbColorModelHex { Val = "9BBB59" }),
                    new A.Accent4Color(new A.RgbColorModelHex { Val = "8064A2" }),
                    new A.Accent5Color(new A.RgbColorModelHex { Val = "4BACC6" }),
                    new A.Accent6Color(new A.RgbColorModelHex { Val = "F79646" }),
                    new A.Hyperlink(new A.RgbColorModelHex { Val = "0000FF" }),
                    new A.FollowedHyperlinkColor(new A.RgbColorModelHex { Val = "800080" }))
                { Name = "Office" },
                new A.FontScheme(
                    new A.MajorFont(
                        new A.LatinFont { Typeface = "Calibri" },
                        new A.EastAsianFont { Typeface = string.Empty },
                        new A.ComplexScriptFont { Typeface = string.Empty }),
                    new A.MinorFont(
                        new A.LatinFont { Typeface = "Calibri" },
                        new A.EastAsianFont { Typeface = string.Empty },
                        new A.ComplexScriptFont { Typeface = string.Empty }))
                { Name = "Office" },
                new A.FormatScheme(
                    new A.FillStyleList(
                        new A.SolidFill(new A.SchemeColor { Val = A.SchemeColorValues.PhColor })),
                    new A.LineStyleList(
                        new A.Outline(
                            new A.SolidFill(new A.SchemeColor { Val = A.SchemeColorValues.PhColor }))
                        { Width = 9525 }),
                    new A.EffectStyleList(new A.EffectStyle(new A.EffectList())),
                    new A.BackgroundFillStyleList(
                        new A.SolidFill(new A.SchemeColor { Val = A.SchemeColorValues.PhColor })))
                { Name = "Office" }))
        { Name = "Office Theme" };
    }
}
