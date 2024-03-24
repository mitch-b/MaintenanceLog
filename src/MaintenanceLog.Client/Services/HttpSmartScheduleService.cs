using MaintenanceLog.Common.Contracts;
using MaintenanceLog.Common.Models.Requests;
using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Client;

namespace MaintenanceLog.Client.Services
{
    public class HttpSmartScheduleService(JsonHttpClient httpClient) : ISmartScheduleService
    {
        private readonly JsonHttpClient _httpClient = httpClient;
        public async Task<TaskType> AddAsync(TaskType entity)
        {
            var response = await _httpClient.PostAsJsonAsync<TaskType, TaskType>("api/task-types", entity);
            return response is null 
                ? throw new Exception("API did not return the created task type") 
                : response;
        }

        public async Task<string?> EstimateCronScheduleForItem(string? itemName, string[]? overridePrompts = null)
        {
            var response = await _httpClient.PostAsJsonAsync<EstimateCronScheduleRequest, string>("api/schedules/estimate-cron-schedule", 
                new EstimateCronScheduleRequest
                {
                    ItemName = itemName,
                    OverridePrompts = overridePrompts
                });
            return response is null 
                ? throw new Exception("API did not return the estimated cron schedule") 
                : response;
        }
    }
}
