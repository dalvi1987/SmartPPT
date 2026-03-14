using SmartPPT.Presentation.Application.Interfaces;
using SmartPPT.Presentation.Application.Mappers;
using SmartPPT.Presentation.Application.Repositories;
using SmartPPT.Presentation.Contracts.DTOs;
using SmartPPT.Presentation.Contracts.Requests;
using SmartPPT.Presentation.Contracts.Responses;
using SmartPPT.Presentation.Contracts.Services;

namespace SmartPPT.Presentation.Application.Services;

public sealed class PresentationService : IPresentationService
{
    private readonly IPresentationOrchestrator _presentationOrchestrator;
    private readonly IPresentationRepository _presentationRepository;

    public PresentationService(
        IPresentationOrchestrator presentationOrchestrator,
        IPresentationRepository presentationRepository)
    {
        _presentationOrchestrator = presentationOrchestrator;
        _presentationRepository = presentationRepository;
    }

    public GeneratePresentationResponse GeneratePresentation(GeneratePresentationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var presentation = _presentationOrchestrator.GeneratePresentation(request);

        return new GeneratePresentationResponse
        {
            PresentationId = presentation.Id.ToString(),
            Status = presentation.Status.ToString()
        };
    }

    public PresentationResponse GetPresentation(GetPresentationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!Guid.TryParse(request.PresentationId, out var presentationId))
        {
            return new PresentationResponse();
        }

        var presentation = _presentationRepository.GetPresentation(presentationId);
        return presentation is null ? new PresentationResponse() : PresentationMapper.ToResponse(presentation);
    }

    public IReadOnlyCollection<PresentationDto> ListPresentations()
    {
        return _presentationRepository
            .ListPresentations()
            .Select(PresentationMapper.ToDto)
            .ToList();
    }
}
