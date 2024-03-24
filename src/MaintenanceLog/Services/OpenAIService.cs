namespace MaintenanceLog.Services;

public interface IOpenAIService
{
    Task<string> GenerateCompletionAsync(string model, string prompt, double temperature = 0.7, int maxTokens = 1000);
}

public class OpenAIService : IOpenAIService
{
    private readonly HttpClient _httpClient;

    public OpenAIService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("OpenAI");
    }

    public async Task<string> GenerateCompletionAsync(string model, string prompt, double temperature = 0.7, int maxTokens = 1000)
    {
        var requestBody = new
        {
            model,
            prompt,
            temperature,
            max_tokens = maxTokens
        };

        var response = await _httpClient.PostAsJsonAsync("chat/completions", requestBody);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }
}
