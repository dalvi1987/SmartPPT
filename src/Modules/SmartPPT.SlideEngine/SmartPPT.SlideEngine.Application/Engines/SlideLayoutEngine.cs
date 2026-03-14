using SmartPPT.SlideEngine.Application.Interfaces;
using SmartPPT.SlideEngine.Application.Services;
using SmartPPT.SlideEngine.Contracts.Elements;
using SmartPPT.SlideEngine.Contracts.Layout;
using SmartPPT.SlideEngine.Contracts.Services;
using SmartPPT.SlideEngine.Contracts.Slides;
using SmartPPT.Template.Contracts.Layouts;
using SmartPPT.Template.Contracts.Patterns;
using SmartPPT.Template.Contracts.Services;

namespace SmartPPT.SlideEngine.Application.Engines;

public sealed class SlideLayoutEngine : ISlideLayoutEngine
{
    private readonly ILayoutCalculator _layoutCalculator;
    private readonly PatternSlotResolver _patternSlotResolver;
    private readonly ITemplateProvider _templateProvider;

    public SlideLayoutEngine(
        ITemplateProvider templateProvider,
        ILayoutCalculator layoutCalculator,
        PatternSlotResolver patternSlotResolver)
    {
        _templateProvider = templateProvider;
        _layoutCalculator = layoutCalculator;
        _patternSlotResolver = patternSlotResolver;
    }

    public RenderableSlideDto GenerateLayout(SlideDto slide)
    {
        ArgumentNullException.ThrowIfNull(slide);

        if (!Guid.TryParse(slide.PatternId, out var patternId))
        {
            return CreateEmptySlide(slide.Title);
        }

        var templateMatch = FindTemplatePattern(patternId);
        if (templateMatch is null)
        {
            return CreateEmptySlide(slide.Title);
        }

        var (templateId, pattern) = templateMatch.Value;
        var layoutGrid = _templateProvider.GetLayoutGrid(templateId);
        if (layoutGrid is null)
        {
            return CreateEmptySlide(slide.Title);
        }

        var positionedElements = _patternSlotResolver
            .ResolveSlots(slide.Elements, pattern.Elements)
            .Select((match, index) => MapPositionedElement(match.Element, match.Slot, layoutGrid, index))
            .ToList();

        return new RenderableSlideDto
        {
            Title = slide.Title,
            PositionedElements = positionedElements
        };
    }

    private (Guid TemplateId, PatternDto Pattern)? FindTemplatePattern(Guid patternId)
    {
        foreach (var template in _templateProvider.GetTemplates())
        {
            var pattern = _templateProvider
                .GetPatterns(template.Id)
                .FirstOrDefault(candidate => candidate.Id == patternId);

            if (pattern is not null)
            {
                return (template.Id, pattern);
            }
        }

        return null;
    }

    private PositionedElementDto MapPositionedElement(
        SlideElementDto element,
        PatternElementDto patternSlot,
        LayoutGridDto layoutGrid,
        int elementIndex)
    {
        var position = _layoutCalculator.CalculatePosition(layoutGrid, patternSlot, elementIndex);

        return new PositionedElementDto
        {
            Content = element.Content,
            ElementType = element.ElementType,
            X = position.X,
            Y = position.Y,
            Width = position.Width,
            Height = position.Height
        };
    }

    private static RenderableSlideDto CreateEmptySlide(string title)
    {
        return new RenderableSlideDto
        {
            Title = title,
            PositionedElements = []
        };
    }
}
