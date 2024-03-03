using System.Net.Http.Json;
using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;

namespace MaintenanceLog.Data.Services.Client
{
    public class HttpPropertyService(HttpClient httpClient) : IPropertyService
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<Property> AddAsync(Property entity)
        {
            var result = await _httpClient
                .PostAsJsonAsync<Property>("api/properties", entity);
            var response = await result.Content.ReadFromJsonAsync<Property>();
            return response is null 
                ? throw new Exception("API did not return the created property") 
                : response;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/properties/{id}");
        }

        public async Task<Property?> FindAsync(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Property>($"api/properties/{id}");
            return result;
        }

        public async Task<List<Property>> GetAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Property>>("api/properties");
            return result ?? new List<Property>();
        }

        public async Task<Property> UpdateAsync(Property entity)
        {
            var result = await _httpClient
                .PutAsJsonAsync($"api/properties", entity);
            var response = await result.Content.ReadFromJsonAsync<Property>();
            return response is null 
                ? throw new Exception("API did not return the updated property") 
                : response;
        }
    }
}
