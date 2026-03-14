using SmartPPT.Agent.Contracts.Services;
using SmartPPT.Presentation.Application.Interfaces;
using SmartPPT.Presentation.Application.Repositories;
using SmartPPT.Presentation.Application.Workflows;
using SmartPPT.Presentation.Contracts.Requests;
using SmartPPT.Presentation.Domain.Presentations;
using SmartPPT.SlideEngine.Contracts.Services;

namespace SmartPPT.Presentation.Application.Orchestrators;

public sealed class PresentationOrchestrator : IPresentationOrchestrator
{
    private readonly IAgentOrchestrator _agentOrchestrator;
    private readonly IPresentationRepository _presentationRepository;
    private readonly ISlideLayoutEngine _slideLayoutEngine;

    public PresentationOrchestrator(
        IAgentOrchestrator agentOrchestrator,
        ISlideLayoutEngine slideLayoutEngine,
        IPresentationRepository presentationRepository)
    {
        _agentOrchestrator = agentOrchestrator;
        _slideLayoutEngine = slideLayoutEngine;
        _presentationRepository = presentationRepository;
    }

    public Presentation GeneratePresentation(GeneratePresentationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var workflow = new PresentationGenerationWorkflow(_agentOrchestrator, _slideLayoutEngine);
        var presentation = workflow.Execute(request.Prompt, request.TemplateId, request.DataSources);

        _presentationRepository.SavePresentation(presentation);

        return presentation;
    }
}
