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
            messages = prompt.Select(p => new { role = "system", content = p }).ToArray(),
            temperature,
            max_tokens = maxTokens
        };

        // look into https://platform.openai.com/docs/api-reference/chat/create#chat-create-response_format

        var response = await _httpClient.PostAsJsonAsync("chat/completions", requestBody);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }
}
