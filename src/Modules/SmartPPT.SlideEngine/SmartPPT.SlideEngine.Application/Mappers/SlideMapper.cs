using SmartPPT.SlideEngine.Contracts.Elements;
using SmartPPT.SlideEngine.Contracts.Slides;
using SmartPPT.SlideEngine.Domain.Elements;
using SmartPPT.SlideEngine.Domain.Slides;

namespace SmartPPT.SlideEngine.Application.Mappers;

public static class SlideMapper
{
    public static SlideDto ToDto(SlideModel slide)
    {
        return new SlideDto
        {
            Title = slide.Title,
            PatternId = slide.PatternId,
            Elements = slide.Elements.Select(ToDto).ToList()
        };
    }

    public static SlideElementDto ToDto(SlideElement element)
    {
        return new SlideElementDto
        {
            Content = element.Content,
            ElementType = (Contracts.Enums.ElementType)element.ElementType,
            SlotName = element.SlotName
        };
    }
}
