using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MaintenanceLog.Data.Services.Client
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

            using var stream = new StreamContent(contentStream);
            stream.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var response = await _httpClient.PostAsync(requestUri, stream);
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(responseStream, _jsonSerializerOptions);
        }

        public async Task PostAsJsonAsync<TRequest>(string requestUri, TRequest content)
        {
            using var contentStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(contentStream, content, _jsonSerializerOptions);
            contentStream.Position = 0;

            using var stream = new StreamContent(contentStream);
            stream.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var response = await _httpClient.PostAsync(requestUri, stream);
            response.EnsureSuccessStatusCode();
        }

        public async Task<TResponse> PutAsJsonAsync<TRequest, TResponse>(string requestUri, TRequest content)
        {
            using var contentStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(contentStream, content, _jsonSerializerOptions);
            contentStream.Position = 0;

            using var stream = new StreamContent(contentStream);
            stream.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var response = await _httpClient.PutAsync(requestUri, stream);
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(responseStream, _jsonSerializerOptions);
        }

        public async Task PutAsJsonAsync<TRequest>(string requestUri, TRequest content)
        {
            using var contentStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(contentStream, content, _jsonSerializerOptions);
            contentStream.Position = 0;

            using var stream = new StreamContent(contentStream);
            stream.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var response = await _httpClient.PutAsync(requestUri, stream);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string requestUri)
        {
            var response = await _httpClient.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }
    }
}
