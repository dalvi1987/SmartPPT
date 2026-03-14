namespace SmartPPT.Agent.Infrastructure.Prompts;

public sealed class PromptTemplates
{
    public string BuildPresentationPlanPrompt(string prompt, string templateId, IReadOnlyCollection<string> dataSources)
    {
        return
            $"Create a presentation plan for template '{templateId}'.{Environment.NewLine}" +
            $"User prompt: {prompt}{Environment.NewLine}" +
            $"Data sources: {string.Join(", ", dataSources)}";
    }

    public string BuildSlidePlanPrompt(string presentationTitle, string patternId)
    {
        return
            $"Create a slide plan for presentation '{presentationTitle}'.{Environment.NewLine}" +
            $"Pattern: {patternId}";
    }

    public string BuildContentPrompt(string patternId, string contentType)
    {
        return
            $"Generate content for pattern '{patternId}'.{Environment.NewLine}" +
            $"Content type: {contentType}";
    }
}
