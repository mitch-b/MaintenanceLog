namespace MaintenanceLog.Common.Contracts;
public interface ISmartTaskDefinitionStepService
{
    public Task<List<string>?> SuggestStepsForTaskDefinition(string? name, string? description = null, IEnumerable<string>? taskDefinitionSteps = null, IEnumerable<string>? overrideSystemPrompts = null);
}
