using SmartPPT.Agent.Contracts.Plans;
using SmartPPT.Agent.Contracts.Prompts;

namespace SmartPPT.Agent.Contracts.Services;

public interface IAgentOrchestrator
{
    PresentationPlanDto GeneratePresentationPlan(PromptDto prompt);
}
