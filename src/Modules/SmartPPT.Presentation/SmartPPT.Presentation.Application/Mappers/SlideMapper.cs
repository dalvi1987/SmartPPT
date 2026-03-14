using SmartPPT.Presentation.Contracts.DTOs;
using SmartPPT.Presentation.Domain.Slides;

namespace SmartPPT.Presentation.Application.Mappers;

public static class SlideMapper
{
    public static SlideDto ToDto(PresentationSlide slide)
    {
        return new SlideDto
        {
            SlideId = slide.SlideId,
            PatternId = slide.PatternId,
            Elements = slide.Elements.ToList()
        };
    }
}
