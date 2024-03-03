using System.Net.Http.Json;
using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;

namespace MaintenanceLog.Data.Services.Client
{
    public class HttpAreaService(HttpClient httpClient) : IAreaService
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<Area> AddAsync(Area entity)
        {
            var result = await _httpClient
                .PostAsJsonAsync<Area>("api/areas", entity);
            var response = await result.Content.ReadFromJsonAsync<Area>();
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
            var result = await _httpClient
                .PutAsJsonAsync($"api/areas", entity);
            var response = await result.Content.ReadFromJsonAsync<Area>();
            return response is null 
                ? throw new Exception("API did not return the updated area") 
                : response;
        }
    }
}
