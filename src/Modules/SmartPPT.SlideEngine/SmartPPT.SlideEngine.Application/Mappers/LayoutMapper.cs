using SmartPPT.SlideEngine.Contracts.Layout;
using SmartPPT.SlideEngine.Domain.Layout;
using SmartPPT.SlideEngine.Domain.Slides;

namespace SmartPPT.SlideEngine.Application.Mappers;

public static class LayoutMapper
{
    public static RenderableSlideDto ToDto(RenderableSlide slide)
    {
        return new RenderableSlideDto
        {
            Title = slide.Title,
            PositionedElements = slide.PositionedElements.Select(ToDto).ToList()
        };
    }

    public static PositionedElementDto ToDto(PositionedElement element)
    {
        return new PositionedElementDto
        {
            Content = string.Empty,
            ElementType = (Contracts.Enums.ElementType)element.ElementType,
            X = element.X,
            Y = element.Y,
            Width = element.Width,
            Height = element.Height
        };
    }
}
