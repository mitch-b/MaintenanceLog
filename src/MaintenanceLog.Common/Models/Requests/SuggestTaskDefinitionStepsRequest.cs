namespace MaintenanceLog.Common.Models.Requests;

public record SuggestTaskDefinitionStepsRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string[]? Steps { get; set; }
    public string[]? OverrideSystemPrompts { get; set; }
}
