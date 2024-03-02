using System.Net.Http.Json;
using MaintenanceLog.Data.Entities;

namespace MaintenanceLog.Data.Services.Client
{
    public class HttpAssetService(HttpClient httpClient) : IAssetService
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<Asset> AddAsync(Asset entity)
        {
            var result = await _httpClient
                .PostAsJsonAsync<Asset>("api/assets", entity);
            var response = await result.Content.ReadFromJsonAsync<Asset>();
            return response is null 
                ? throw new Exception("API did not return the created asset") 
                : response;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/assets/{id}");
        }

        public async Task<Asset?> FindAsync(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Asset>($"api/assets/{id}");
            return result;
        }

        public async Task<List<Asset>> GetAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Asset>>("api/assets");
            return result ?? new List<Asset>();
        }

        public async Task<List<Asset>> GetByPropertyAsync(int propertyId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Asset>>($"api/properties/{propertyId}/assets");
            return result ?? new List<Asset>();
        }

        public async Task<List<Asset>> GetByAreaAsync(int areaId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<Asset>>($"api/areas/{areaId}/assets");
            return result ?? new List<Asset>();
        }

        public async Task<Asset> UpdateAsync(Asset entity)
        {
            var result = await _httpClient
                .PutAsJsonAsync($"api/assets", entity);
            var response = await result.Content.ReadFromJsonAsync<Asset>();
            return response is null 
                ? throw new Exception("API did not return the updated asset") 
                : response;
        }
    }
}
