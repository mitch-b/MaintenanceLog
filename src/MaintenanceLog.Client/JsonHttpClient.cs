using System.Text.Json;
using System.Text.Json.Serialization;

namespace MaintenanceLog.Client
{
    public class JsonHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public JsonHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<T> GetFromJsonAsync<T>(string requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            using var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(contentStream, _jsonSerializerOptions);
        }

        public async Task<TResponse> PostAsJsonAsync<TRequest, TResponse>(string requestUri, TRequest content)
        {
            using var contentStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(contentStream, content, _jsonSerializerOptions);
            contentStream.Position = 0;

            using var response = await _httpClient.PostAsync(requestUri, new StreamContent(contentStream));
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(responseStream, _jsonSerializerOptions);
        }

        public async Task PostAsJsonAsync<TRequest>(string requestUri, TRequest content)
        {
            using var contentStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(contentStream, content, _jsonSerializerOptions);
            contentStream.Position = 0;

            using var response = await _httpClient.PostAsync(requestUri, new StreamContent(contentStream));
            response.EnsureSuccessStatusCode();
        }

        public async Task<TResponse> PutAsJsonAsync<TRequest, TResponse>(string requestUri, TRequest content)
        {
            using var contentStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(contentStream, content, _jsonSerializerOptions);
            contentStream.Position = 0;

            using var response = await _httpClient.PutAsync(requestUri, new StreamContent(contentStream));
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(responseStream, _jsonSerializerOptions);
        }

        public async Task PutAsJsonAsync<TRequest>(string requestUri, TRequest content)
        {
            using var contentStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(contentStream, content, _jsonSerializerOptions);
            contentStream.Position = 0;

            using var response = await _httpClient.PutAsync(requestUri, new StreamContent(contentStream));
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string requestUri)
        {
            var response = await _httpClient.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }
    }
}
