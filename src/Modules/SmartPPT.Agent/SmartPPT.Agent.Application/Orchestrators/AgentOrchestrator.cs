using SmartPPT.Agent.Contracts.Plans;
using SmartPPT.Agent.Contracts.Prompts;
using SmartPPT.Agent.Contracts.Services;

namespace SmartPPT.Agent.Application.Orchestrators;

public sealed class AgentOrchestrator : IAgentOrchestrator
{
    public PresentationPlanDto GeneratePresentationPlan(PromptDto prompt)
    {
        ArgumentNullException.ThrowIfNull(prompt);

        return new PresentationPlanDto
        {
            Title = string.IsNullOrWhiteSpace(prompt.Text) ? "Untitled Presentation" : prompt.Text,
            Slides = []
        };
    }
}
