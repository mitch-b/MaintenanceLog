namespace MaintenanceLog.Common.Contracts;
public interface ISmartTaskDefinitionStepService
{
    public Task<List<string>?> SuggestStepsForTaskDefinition(string? itemName, string? description = null, List<string>? taskDefinitionSteps = null);
}
