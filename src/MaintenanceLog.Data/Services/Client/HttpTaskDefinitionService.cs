using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;

namespace MaintenanceLog.Data.Services.Client
{
    public class HttpTaskDefinitionService(JsonHttpClient httpClient) : ITaskDefinitionService
    {
        private readonly JsonHttpClient _httpClient = httpClient;
        public async Task<TaskDefinition> AddAsync(TaskDefinition entity)
        {
            var response = await _httpClient.PostAsJsonAsync<TaskDefinition, TaskDefinition>("api/task-definitions", entity);
            return response is null 
                ? throw new Exception("API did not return the created task definition") 
                : response;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/task-definitions/{id}");
        }

        public async Task<TaskDefinition?> FindAsync(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<TaskDefinition>($"api/task-definitions/{id}");
            return result;
        }

        public async Task<List<TaskDefinition>> GetAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskDefinition>>("api/task-definitions");
            return result ?? new List<TaskDefinition>();
        }

        public async Task<List<TaskDefinition>> GetByPropertyAsync(int propertyId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskDefinition>>($"api/properties/{propertyId}/task-definitions");
            return result ?? new List<TaskDefinition>();
        }

        public async Task<List<TaskDefinition>> GetByAreaAsync(int areaId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskDefinition>>($"api/areas/{areaId}/task-definitions");
            return result ?? new List<TaskDefinition>();
        }

        public async Task<TaskDefinition> UpdateAsync(TaskDefinition entity)
        {
            var response = await _httpClient
                .PutAsJsonAsync<TaskDefinition, TaskDefinition>($"api/task-definitions", entity);
            return response is null 
                ? throw new Exception("API did not return the updated task definition") 
                : response;
        }

        public async Task<List<TaskDefinition>> GetByAssetAsync(int assetId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskDefinition>>($"api/assets/{assetId}/task-definitions");
            return result ?? new List<TaskDefinition>();
        }

        public async Task<List<TaskDefinition>> GetByTaskTypeAsync(int taskTypeId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskDefinition>>($"api/task-types/{taskTypeId}/task-definitions");
            return result ?? new List<TaskDefinition>();
        }
    }
}
