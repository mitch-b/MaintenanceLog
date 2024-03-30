using MaintenanceLog.Common.Contracts;
using MaintenanceLog.Common.Models.Requests;
using MaintenanceLog.Common.Models.Responses;
using MaintenanceLog.Data.Services.Client;

namespace MaintenanceLog.Client.Services
{
    public class HttpSmartScheduleService(JsonHttpClient httpClient) : ISmartScheduleService
    {
        private readonly JsonHttpClient _httpClient = httpClient;

        public async Task<string?> EstimateCronScheduleForItem(string? itemName, string[]? overridePrompts = null)
        {
            var response = await _httpClient.PostAsJsonAsync<EstimateCronScheduleRequest, EstimateCronScheduleResponse>("api/schedules/estimate-cron-schedule", 
                new EstimateCronScheduleRequest
                {
                    ItemName = itemName,
                    OverrideSystemPrompts = overridePrompts
                });
            return response is null 
                ? throw new Exception("API did not return the estimated Cron schedule") 
                : response.CronExpression;
        }
    }
}
