using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;

namespace MaintenanceLog.Data.Services.Client
{
    public class HttpAreaService(JsonHttpClient httpClient) : IAreaService
    {
        private readonly JsonHttpClient _httpClient = httpClient;
        public async Task<Area> AddAsync(Area entity)
        {
            var response = await _httpClient.PostAsJsonAsync<Area, Area>("api/areas", entity);
            return response is null 
                ? throw new Exception("API did not return the created area") 
                : response;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/areas/{id}");
        }

        public async Task<Area?> FindAsync(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Area>($"api/areas/{id}");
            return result;
        }

        public async Task<List<Area>> GetAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Area>>("api/areas");
            return result ?? new List<Area>();
        }

        public async Task<List<Area>> GetByPropertyAsync(int propertyId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Area>>($"api/properties/{propertyId}/areas");
            return result ?? new List<Area>();
        }

        public async Task<Area> UpdateAsync(Area entity)
        {
            var response = await _httpClient
                .PutAsJsonAsync<Area, Area>($"api/areas", entity);
            return response is null 
                ? throw new Exception("API did not return the updated area") 
                : response;
        }
    }
}
