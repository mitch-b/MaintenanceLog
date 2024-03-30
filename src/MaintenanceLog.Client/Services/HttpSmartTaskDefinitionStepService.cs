using MaintenanceLog.Common.Contracts;
using MaintenanceLog.Common.Models.Requests;
using MaintenanceLog.Common.Models.Responses;
using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Client;

namespace MaintenanceLog.Client.Services
{
    public class HttpSmartTaskDefinitionStepService(JsonHttpClient httpClient) : ISmartTaskDefinitionStepService
    {
        private readonly JsonHttpClient _httpClient = httpClient;

        public async Task<List<string>?> SuggestStepsForTaskDefinition(string? itemName, string? description = null, List<string>? taskDefinitionSteps = null)        
        {
            var response = await _httpClient.PostAsJsonAsync<SuggestTaskDefinitionStepsRequest, SuggestTaskDefinitionStepsResponse>("api/task-definitions/estimate-task-definition-steps", 
                new SuggestTaskDefinitionStepsRequest
                {
                    ItemName = itemName,
                    Description = description,
                    Steps = taskDefinitionSteps
                });
            return response is null 
                ? throw new Exception("API did not return suggested steps") 
                : response.Steps;
        }
    }
}
