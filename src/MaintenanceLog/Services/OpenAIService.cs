using System.Text.Json;

namespace MaintenanceLog.Services;

public interface IOpenAIService
{
    Task<string> GenerateCompletionAsync(string model, List<string> prompt, double temperature = 0.7, int maxTokens = 1000);
}

public class OpenAIService : IOpenAIService
{
    private readonly HttpClient _httpClient;

    public OpenAIService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("OpenAI");
    }

    public async Task<string> GenerateCompletionAsync(string model, List<string> prompt, double temperature = 0.7, int maxTokens = 1000)
    {
        var messages = new List<Message>();
        for (var i = 0; i < prompt.Count - 1; i++)
        {
            messages.Add(new Message { Role = "system", Content = prompt[i] });
        }
        messages.Add(new Message { Role = "user", Content = prompt.Last() });

        var requestBody = new
        {
            model,
            messages,
            temperature,
            max_tokens = maxTokens
        };

        // look into https://platform.openai.com/docs/api-reference/chat/create#chat-create-response_format

        var response = await _httpClient.PostAsJsonAsync("chat/completions", requestBody);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Raw OpenAI Response: {responseContent}");

        var responseObject = JsonSerializer.Deserialize<ChatResponse>(responseContent, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        });
        var aiResponse = responseObject?.Choices?.Last()?.Message?.Content;
        Console.WriteLine($"Parsed OpenAI Response: {aiResponse ?? ""}");
        return aiResponse ?? "";
    }
}

public class Message
{
    public string Role { get; set; }
    public string Content { get; set; }
}

public class Choice
{
    public int Index { get; set; }
    public Message Message { get; set; }
}

public class ChatResponse
{
    public string Id { get; set; }
    public string Object { get; set; }
    public string Model { get; set; }
    public List<Choice> Choices { get; set; }
}
