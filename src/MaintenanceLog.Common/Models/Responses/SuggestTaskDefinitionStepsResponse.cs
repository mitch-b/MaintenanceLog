namespace MaintenanceLog.Common.Models.Responses;

public record SuggestTaskDefinitionStepsResponse
{
    public IEnumerable<string>? Steps { get; set; }
}
