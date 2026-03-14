using System.Collections.Generic;
using SmartPPT.Presentation.Contracts.DTOs;
using SmartPPT.Presentation.Contracts.Requests;
using SmartPPT.Presentation.Contracts.Responses;

namespace SmartPPT.Presentation.Contracts.Services;

public interface IPresentationService
{
    GeneratePresentationResponse GeneratePresentation(GeneratePresentationRequest request);

    PresentationResponse GetPresentation(GetPresentationRequest request);

    IReadOnlyCollection<PresentationDto> ListPresentations();
}
