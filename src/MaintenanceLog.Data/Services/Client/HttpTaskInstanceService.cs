using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;

namespace MaintenanceLog.Data.Services.Client
{
    public class HttpTaskInstanceService(JsonHttpClient httpClient) : ITaskInstanceService
    {
        private readonly JsonHttpClient _httpClient = httpClient;
        public async Task<TaskInstance> AddAsync(TaskInstance entity)
        {
            var response = await _httpClient.PostAsJsonAsync<TaskInstance, TaskInstance>("api/task-instances", entity);
            return response is null 
                ? throw new Exception("API did not return the created task instance") 
                : response;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/task-instances/{id}");
        }

        public async Task<TaskInstance?> FindAsync(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<TaskInstance>($"api/task-instances/{id}");
            return result;
        }

        public async Task<List<TaskInstance>> GetAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskInstance>>("api/task-instances");
            return result ?? new List<TaskInstance>();
        }

        public async Task<List<TaskInstance>> GetByPropertyAsync(int propertyId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskInstance>>($"api/properties/{propertyId}/task-instances");
            return result ?? new List<TaskInstance>();
        }

        public async Task<List<TaskInstance>> GetByAreaAsync(int areaId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskInstance>>($"api/areas/{areaId}/task-instances");
            return result ?? new List<TaskInstance>();
        }

        public async Task<TaskInstance> UpdateAsync(TaskInstance entity)
        {
            var response = await _httpClient
                .PutAsJsonAsync<TaskInstance, TaskInstance>($"api/task-instances", entity);
            return response is null 
                ? throw new Exception("API did not return the updated task instance") 
                : response;
        }

        public async Task<List<TaskInstance>> GetByAssetAsync(int assetId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskInstance>>($"api/assets/{assetId}/task-instances");
            return result ?? new List<TaskInstance>();
        }

        public async Task<List<TaskInstance>> GetByTaskTypeAsync(int taskTypeId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskInstance>>($"api/task-types/{taskTypeId}/task-instances");
            return result ?? new List<TaskInstance>();
        }

        public async Task<List<TaskInstance>> GetByTaskDefinitionAsync(int taskDefinitionId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskInstance>>($"api/task-definitions/{taskDefinitionId}/task-instances");
            return result ?? new List<TaskInstance>();
        }
    }
}
