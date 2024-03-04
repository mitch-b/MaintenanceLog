using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;

namespace MaintenanceLog.Data.Services.Client
{
    public class HttpTaskTypeService(JsonHttpClient httpClient) : ITaskTypeService
    {
        private readonly JsonHttpClient _httpClient = httpClient;
        public async Task<TaskType> AddAsync(TaskType entity)
        {
            var response = await _httpClient.PostAsJsonAsync<TaskType, TaskType>("api/task-types", entity);
            return response is null 
                ? throw new Exception("API did not return the created task type") 
                : response;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/task-types/{id}");
        }

        public async Task<TaskType?> FindAsync(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<TaskType>($"api/task-types/{id}");
            return result;
        }

        public async Task<List<TaskType>> GetAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskType>>("api/task-types");
            return result ?? new List<TaskType>();
        }

        public async Task<TaskType> UpdateAsync(TaskType entity)
        {
            var response = await _httpClient
                .PutAsJsonAsync<TaskType, TaskType>($"api/task-types", entity);
            return response is null 
                ? throw new Exception("API did not return the updated task type") 
                : response;
        }
    }
}
