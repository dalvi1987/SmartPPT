using SmartPPT.Presentation.Contracts.Requests;
using SmartPPT.Presentation.Domain.Presentations;

namespace SmartPPT.Presentation.Application.Interfaces;

public interface IPresentationOrchestrator
{
    Presentation GeneratePresentation(GeneratePresentationRequest request);
}
