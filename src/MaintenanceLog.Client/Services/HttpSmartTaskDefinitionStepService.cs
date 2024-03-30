using MaintenanceLog.Common.Contracts;
using MaintenanceLog.Common.Models.Requests;
using MaintenanceLog.Common.Models.Responses;
using MaintenanceLog.Data.Services.Client;

namespace MaintenanceLog.Client.Services
{
    public class HttpSmartTaskDefinitionStepService(JsonHttpClient httpClient) : ISmartTaskDefinitionStepService
    {
        private readonly JsonHttpClient _httpClient = httpClient;

        public async Task<List<string>?> SuggestStepsForTaskDefinition(string? name, string? description = null, IEnumerable<string>? taskDefinitionSteps = null, IEnumerable<string>? overrideSystemPrompts = null)
        {
            var response = await _httpClient.PostAsJsonAsync<SuggestTaskDefinitionStepsRequest, SuggestTaskDefinitionStepsResponse>("api/task-definition-steps/suggest",
                new SuggestTaskDefinitionStepsRequest
                {
                    Name = name,
                    Description = description,
                    OverrideSystemPrompts = overrideSystemPrompts?.ToArray(),
                    Steps = taskDefinitionSteps?.ToArray()
                });
            return response is null 
                ? throw new Exception("API did not return suggested steps") 
                : response.Steps?.ToList();
        }
    }
}
