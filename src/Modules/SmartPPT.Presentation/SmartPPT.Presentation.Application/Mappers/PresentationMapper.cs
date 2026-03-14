using SmartPPT.Presentation.Contracts.DTOs;
using SmartPPT.Presentation.Contracts.Responses;
using SmartPPT.Presentation.Domain.Presentations;

namespace SmartPPT.Presentation.Application.Mappers;

public static class PresentationMapper
{
    public static PresentationDto ToDto(Presentation presentation)
    {
        return new PresentationDto
        {
            Id = presentation.Id.ToString(),
            Title = presentation.Title,
            TemplateId = presentation.TemplateId,
            Status = presentation.Status.ToString(),
            SlideCount = presentation.Slides.Count,
            CreatedAt = presentation.CreatedAt
        };
    }

    public static PresentationResponse ToResponse(Presentation presentation)
    {
        return new PresentationResponse
        {
            PresentationId = presentation.Id.ToString(),
            Title = presentation.Title,
            TemplateId = presentation.TemplateId,
            Slides = presentation.Slides.Select(SlideMapper.ToDto).ToList(),
            Status = presentation.Status.ToString(),
            CreatedAt = presentation.CreatedAt
        };
    }
}
