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
    private const int SlideWidth = 9144000;
    private const int SlideHeight = 6858000;
    private const long TitleX = 457200L;
    private const long TitleY = 274320L;
    private const long TitleWidth = 8229600L;
    private const long TitleHeight = 822960L;
    private const long ContentX = 640080L;
    private const long ContentY = 1371600L;
    private const long ContentWidth = 7863840L;
    private const long ContentHeight = 4572000L;

    private readonly StorageOptions _options;

    public PptDocumentBuilder(IOptions<StorageOptions> options)
    {
        _options = options.Value;
    }

    public StoredFile BuildDocument(List<RenderableSlideDto> slides)
    {
        ArgumentNullException.ThrowIfNull(slides);

        var documentsDirectory = Path.Combine(_options.StoragePath, "Documents");
        Directory.CreateDirectory(documentsDirectory);

        var fileName = $"{Guid.NewGuid()}.pptx";
        var filePath = Path.Combine(documentsDirectory, fileName);
        var createdAt = DateTime.UtcNow;

        using (var document = PresentationDocument.Create(filePath, PresentationDocumentType.Presentation))
        {
            var presentationPart = document.AddPresentationPart();
            presentationPart.Presentation = new P.Presentation();

            var slideMasterPart = CreateSlideMasterPart(presentationPart);
            var slideLayoutPart = slideMasterPart.SlideLayoutParts.First();

            var slideMasterIdList = presentationPart.Presentation.AppendChild(new P.SlideMasterIdList());
            slideMasterIdList.Append(new P.SlideMasterId
            {
                Id = 2147483648U,
                RelationshipId = presentationPart.GetIdOfPart(slideMasterPart)
            });

            var slideIdList = presentationPart.Presentation.AppendChild(new P.SlideIdList());
            presentationPart.Presentation.SlideSize = new P.SlideSize { Cx = SlideWidth, Cy = SlideHeight };
            presentationPart.Presentation.NotesSize = new P.NotesSize { Cx = SlideHeight, Cy = SlideWidth };

            uint slideId = 256U;

            foreach (var slide in slides)
            {
                var slidePart = presentationPart.AddNewPart<SlidePart>();
                CreateSlidePart(slidePart, slide, slideLayoutPart);

                slideIdList.Append(new P.SlideId
                {
                    Id = slideId++,
                    RelationshipId = presentationPart.GetIdOfPart(slidePart)
                });
            }

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
            new P.CommonSlideData(CreateBaseShapeTree()),
            new P.ColorMapOverride(new A.MasterColorMapping()))
        {
            Type = P.SlideLayoutValues.Blank,
            Preserve = true
        };
        slideLayoutPart.SlideLayout.Save();

        slideMasterPart.SlideMaster = new P.SlideMaster(
            new P.CommonSlideData(CreateBaseShapeTree()),
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

    private static void CreateSlidePart(SlidePart slidePart, RenderableSlideDto slide, SlideLayoutPart slideLayoutPart)
    {
        slidePart.AddPart(slideLayoutPart);

        var shapeTree = CreateBaseShapeTree();
        shapeTree.Append(CreateTextShape(string.IsNullOrWhiteSpace(slide.Title) ? "Untitled Slide" : slide.Title, 2U));

        var bulletTexts = slide.PositionedElements
            .Where(element => element.ElementType == ElementType.Text && !string.IsNullOrWhiteSpace(element.Content))
            .Select(element => element.Content)
            .ToList();

        shapeTree.Append(CreateBulletShape(bulletTexts, 3U));

        slidePart.Slide = new P.Slide(
            new P.CommonSlideData(shapeTree),
            new P.ColorMapOverride(new A.MasterColorMapping()));

        slidePart.Slide.Save();
    }

    private static P.ShapeTree CreateBaseShapeTree()
    {
        return new P.ShapeTree(
            new P.NonVisualGroupShapeProperties(
                new P.NonVisualDrawingProperties { Id = 1U, Name = string.Empty },
                new P.NonVisualGroupShapeDrawingProperties(),
                new P.ApplicationNonVisualDrawingProperties()),
            new P.GroupShapeProperties(
                new A.TransformGroup(
                    new A.Offset { X = 0L, Y = 0L },
                    new A.Extents { Cx = 0L, Cy = 0L },
                    new A.ChildOffset { X = 0L, Y = 0L },
                    new A.ChildExtents { Cx = 0L, Cy = 0L })));
    }

    private static P.Shape CreateTextShape(string text, uint id)
    {
        return new P.Shape(
            new P.NonVisualShapeProperties(
                new P.NonVisualDrawingProperties { Id = id, Name = $"TextBox {id}" },
                new P.NonVisualShapeDrawingProperties(new A.ShapeLocks { NoGrouping = true }),
                new P.ApplicationNonVisualDrawingProperties()),
            new P.ShapeProperties(
                new A.Transform2D(
                    new A.Offset { X = TitleX, Y = TitleY },
                    new A.Extents { Cx = TitleWidth, Cy = TitleHeight }),
                new A.PresetGeometry(new A.AdjustValueList()) { Preset = A.ShapeTypeValues.Rectangle }),
            new P.TextBody(
                new A.BodyProperties(),
                new A.ListStyle(),
                CreateParagraph(text, isBullet: false, fontSize: 2400)));
    }

    private static P.Shape CreateBulletShape(List<string> texts, uint id)
    {
        var textBody = new P.TextBody(
            new A.BodyProperties(),
            new A.ListStyle());

        if (texts.Count == 0)
        {
            textBody.Append(CreateParagraph(string.Empty, isBullet: false, fontSize: 1800));
        }
        else
        {
            foreach (var text in texts)
            {
                textBody.Append(CreateParagraph(text, isBullet: true, fontSize: 1800));
            }
        }

        return new P.Shape(
            new P.NonVisualShapeProperties(
                new P.NonVisualDrawingProperties { Id = id, Name = $"TextBox {id}" },
                new P.NonVisualShapeDrawingProperties(new A.ShapeLocks { NoGrouping = true }),
                new P.ApplicationNonVisualDrawingProperties()),
            new P.ShapeProperties(
                new A.Transform2D(
                    new A.Offset { X = ContentX, Y = ContentY },
                    new A.Extents { Cx = ContentWidth, Cy = ContentHeight }),
                new A.PresetGeometry(new A.AdjustValueList()) { Preset = A.ShapeTypeValues.Rectangle }),
            textBody);
    }

    private static A.Paragraph CreateParagraph(string text, bool isBullet, int fontSize)
    {
        var paragraph = new A.Paragraph();
        var paragraphProperties = new A.ParagraphProperties();

        if (isBullet)
        {
            paragraphProperties.Level = 0;
            paragraphProperties.LeftMargin = 342900;
            paragraphProperties.Indent = -171450;
            paragraphProperties.Append(new A.BulletFont { Typeface = "Arial" });
            paragraphProperties.Append(new A.CharacterBullet { Char = "•" });
        }

        paragraph.Append(paragraphProperties);
        paragraph.Append(new A.Run(
            new A.RunProperties { Language = "en-US", FontSize = fontSize },
            new A.Text(text)));
        paragraph.Append(new A.EndParagraphRunProperties { Language = "en-US", FontSize = fontSize });

        return paragraph;
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
