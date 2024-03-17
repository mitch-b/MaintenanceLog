using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;

namespace MaintenanceLog.Data.Services.Client
{
    public class HttpTaskInstanceStepService(JsonHttpClient httpClient) : ITaskInstanceStepService
    {
        private readonly JsonHttpClient _httpClient = httpClient;
        public async Task<TaskInstanceStep> AddAsync(TaskInstanceStep entity)
        {
            var response = await _httpClient.PostAsJsonAsync<TaskInstanceStep, TaskInstanceStep>("api/task-instance-steps", entity);
            return response is null 
                ? throw new Exception("API did not return the created TaskInstanceStep") 
                : response;
        }

        public async Task<List<TaskInstanceStep>> CreateFromTaskDefinitionAsync(int taskInstanceId, int taskDefinitionId)
        {
            var response = await _httpClient.PostAsJsonAsync<List<TaskInstanceStep>>($"api/task-definitions/{taskDefinitionId}/task-instances/{taskInstanceId}/task-instance-steps");
            return response is null
                ? throw new Exception("API did not return the created TaskInstanceStep")
                : response;
        }

        public async Task<TaskInstanceStep> CreateFromTaskDefinitionStepAsync(int taskInstanceId, int taskDefinitionStepId)
        {
            var response = await _httpClient.PostAsJsonAsync<TaskInstanceStep>($"api/task-definition-steps/{taskDefinitionStepId}/task-instances/{taskInstanceId}/task-instance-step");
            return response is null
                ? throw new Exception("API did not return the created TaskInstanceStep")
                : response;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/task-instance-steps/{id}");
        }

        public async Task<TaskInstanceStep?> FindAsync(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<TaskInstanceStep>($"api/task-instance-steps/{id}");
            return result;
        }

        public async Task<List<TaskInstanceStep>> GetAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskInstanceStep>>("api/task-instance-steps");
            return result ?? [];
        }

        public async Task<List<TaskInstanceStep>> GetByTaskInstanceAsync(int taskInstanceId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskInstanceStep>>($"api/task-instances/{taskInstanceId}/task-instance-steps");
            return result ?? [];
        }

        public async Task<TaskInstanceStep> UpdateAsync(TaskInstanceStep entity)
        {
            var response = await _httpClient
                .PutAsJsonAsync<TaskInstanceStep, TaskInstanceStep>($"api/task-instance-steps", entity);
            return response is null 
                ? throw new Exception("API did not return the updated TaskInstanceStep") 
                : response;
        }
    }
}
