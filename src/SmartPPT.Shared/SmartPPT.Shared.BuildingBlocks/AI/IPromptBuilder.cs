namespace SmartPPT.Shared.BuildingBlocks.AI;

public interface IPromptBuilder
{
    string Build();

    string Build(IReadOnlyDictionary<string, object?> variables);
}
