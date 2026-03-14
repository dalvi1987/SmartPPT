using SmartPPT.Agent.Contracts.Plans;
using SmartPPT.Agent.Contracts.Prompts;
using SmartPPT.Agent.Contracts.Services;
using SmartPPT.Presentation.Domain.Enums;
using SmartPPT.Presentation.Domain.Presentations;
using SmartPPT.Presentation.Domain.Slides;
using SmartPPT.SlideEngine.Contracts.Elements;
using SmartPPT.SlideEngine.Contracts.Services;
using SmartPPT.SlideEngine.Contracts.Slides;

namespace SmartPPT.Presentation.Application.Workflows;

public sealed class PresentationGenerationWorkflow
{
    private readonly IAgentOrchestrator _agentOrchestrator;
    private readonly ISlideLayoutEngine _slideLayoutEngine;

    public PresentationGenerationWorkflow(
        IAgentOrchestrator agentOrchestrator,
        ISlideLayoutEngine slideLayoutEngine)
    {
        _agentOrchestrator = agentOrchestrator;
        _slideLayoutEngine = slideLayoutEngine;
    }

    public Presentation Execute(string prompt, string templateId, IReadOnlyCollection<string> dataSources)
    {
        var plan = GeneratePresentationPlan(prompt, templateId, dataSources);
        var presentation = CreatePresentation(plan, templateId);

        foreach (var slidePlan in plan.Slides)
        {
            var renderableSlide = _slideLayoutEngine.GenerateLayout(ToSlideDto(slidePlan));
            presentation.Slides.Add(new PresentationSlide
            {
                SlideId = Guid.NewGuid().ToString(),
                PatternId = slidePlan.PatternId,
                Elements = renderableSlide.PositionedElements
                    .Select(element => element.Content)
                    .Where(content => !string.IsNullOrWhiteSpace(content))
                    .ToList()
            });
        }

        // Placeholder until Storage.Contracts exposes a presentation output contract.
        PublishPresentationArtifact(presentation);

        presentation.Status = PresentationStatus.Completed;

        return presentation;
    }

    private PresentationPlanDto GeneratePresentationPlan(
        string prompt,
        string templateId,
        IReadOnlyCollection<string> dataSources)
    {
        return _agentOrchestrator.GeneratePresentationPlan(new PromptDto
        {
            Text = prompt,
            TemplateId = templateId,
            DataSources = dataSources.ToList()
        });
    }

    private static Presentation CreatePresentation(PresentationPlanDto plan, string templateId)
    {
        return new Presentation
        {
            Id = Guid.NewGuid(),
            Title = plan.Title,
            TemplateId = templateId,
            CreatedAt = DateTime.UtcNow,
            Status = PresentationStatus.Generating,
            Slides = []
        };
    }

    private static SlideDto ToSlideDto(SlidePlanDto slidePlan)
    {
        return new SlideDto
        {
            Title = string.Empty,
            PatternId = slidePlan.PatternId,
            Elements = slidePlan.Contents.Select(content => new SlideElementDto
            {
                Content = content.Value,
                SlotName = string.Empty,
                ElementType = MapElementType(content.ContentType)
            }).ToList()
        };
    }

    private static SmartPPT.SlideEngine.Contracts.Enums.ElementType MapElementType(
        SmartPPT.Agent.Contracts.Enums.ContentType contentType)
    {
        return contentType switch
        {
            SmartPPT.Agent.Contracts.Enums.ContentType.ChartData => SmartPPT.SlideEngine.Contracts.Enums.ElementType.Chart,
            SmartPPT.Agent.Contracts.Enums.ContentType.TableData => SmartPPT.SlideEngine.Contracts.Enums.ElementType.Table,
            SmartPPT.Agent.Contracts.Enums.ContentType.Image => SmartPPT.SlideEngine.Contracts.Enums.ElementType.Image,
            _ => SmartPPT.SlideEngine.Contracts.Enums.ElementType.Text
        };
    }

    private static void PublishPresentationArtifact(Presentation presentation)
    {
    }
}
