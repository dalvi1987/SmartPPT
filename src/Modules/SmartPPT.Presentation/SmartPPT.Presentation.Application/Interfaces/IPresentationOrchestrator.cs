using SmartPPT.Presentation.Contracts.Requests;
using prsnt = SmartPPT.Presentation.Domain.Presentations;

namespace SmartPPT.Presentation.Application.Interfaces;

public interface IPresentationOrchestrator
{
    prsnt.Presentation GeneratePresentation(GeneratePresentationRequest request);
}
