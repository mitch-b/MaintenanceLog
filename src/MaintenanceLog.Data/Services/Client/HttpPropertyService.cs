using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;

namespace MaintenanceLog.Data.Services.Client
{
    public class HttpPropertyService(JsonHttpClient httpClient) : IPropertyService
    {
        private readonly JsonHttpClient _httpClient = httpClient;
        public async Task<Property> AddAsync(Property entity)
        {
            var response = await _httpClient
                .PostAsJsonAsync<Property, Property>("api/properties", entity);
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
            var response = await _httpClient
                .PutAsJsonAsync<Property, Property>($"api/properties", entity);
            return response is null 
                ? throw new Exception("API did not return the updated property") 
                : response;
        }
    }
}
