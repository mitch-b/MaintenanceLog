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

        public async Task<List<TaskDefinitionStep>?> SuggestStepsForTaskDefinition(string? itemName, string? description = null, List<TaskDefinitionStep>? taskDefinitionSteps = null)        
        {
            var response = await _httpClient.PostAsJsonAsync<SuggestTaskDefinitionStepsRequest, SuggestTaskDefinitionStepsResponse>("api/task-definitions/estimate-task-definition-steps", 
                new SuggestTaskDefinitionStepsRequest
                {
                    ItemName = itemName,
                    Description = description,
                    TaskDefinitionSteps = taskDefinitionSteps
                });
            return response is null 
                ? throw new Exception("API did not return the estimated Cron schedule") 
                : response.TaskDefinitionSteps;
        }
    }
}
