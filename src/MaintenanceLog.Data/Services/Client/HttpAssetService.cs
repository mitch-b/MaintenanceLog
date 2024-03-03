using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;

namespace MaintenanceLog.Data.Services.Client
{
    public class HttpAssetService(JsonHttpClient httpClient) : IAssetService
    {
        private readonly JsonHttpClient _httpClient = httpClient;
        public async Task<Asset> AddAsync(Asset entity)
        {
            var response = await _httpClient
                .PostAsJsonAsync<Asset, Asset>("api/assets", entity);
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
            var response = await _httpClient
                .PutAsJsonAsync<Asset, Asset>($"api/assets", entity);
            return response is null 
                ? throw new Exception("API did not return the updated asset") 
                : response;
        }
    }
}
