using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;

namespace MaintenanceLog.Data.Services.Client
{
    public class HttpTaskDefinitionStepService(JsonHttpClient httpClient) : ITaskDefinitionStepService
    {
        private readonly JsonHttpClient _httpClient = httpClient;
        public async Task<TaskDefinitionStep> AddAsync(TaskDefinitionStep entity)
        {
            var response = await _httpClient.PostAsJsonAsync<TaskDefinitionStep, TaskDefinitionStep>("api/task-definition-steps", entity);
            return response is null 
                ? throw new Exception("API did not return the created TaskDefinitionStep") 
                : response;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/task-definition-steps/{id}");
        }

        public async Task<TaskDefinitionStep?> FindAsync(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<TaskDefinitionStep>($"api/task-definition-steps/{id}");
            return result;
        }

        public async Task<List<TaskDefinitionStep>> GetAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskDefinitionStep>>("api/task-definition-steps");
            return result ?? [];
        }

        public async Task<List<TaskDefinitionStep>> GetByTaskDefinitionAsync(int taskDefinitionId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskDefinitionStep>>($"api/task-definition/{taskDefinitionId}/task-definition-steps");
            return result ?? [];
        }

        public async Task<TaskDefinitionStep> UpdateAsync(TaskDefinitionStep entity)
        {
            var response = await _httpClient
                .PutAsJsonAsync<TaskDefinitionStep, TaskDefinitionStep>($"api/task-definition-steps", entity);
            return response is null 
                ? throw new Exception("API did not return the updated TaskDefinitionStep") 
                : response;
        }
    }
}
