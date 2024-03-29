using System.Text.Json;
using MaintenanceLog.Common.Contracts;

namespace MaintenanceLog.Services;

public class OpenAISmartScheduleService(IOpenAIService openAIService) : ISmartScheduleService
{
    private readonly IOpenAIService _openAIService = openAIService;

    public async Task<string?> EstimateCronScheduleForItem(string? itemName, string[]? overridePrompts = null)
    {
        var prompts = overridePrompts ?? 
        [
            "You help return Cron expressions based on questions.",
            "Please return your response in a JSON object which includes a Cron expression.",
            $"Format should match the following example: {JsonSerializer.Serialize(new CronExpressionSuggestion("10 0 1 * *"))}",
            "Assume a person's day starts at 9AM local time and be mindful of how often this type of activity should happen.",
            "Only request every week if completely necessary.",
            $"Given typical situations, how frequently should \"{itemName}\" happen?"
        ];

        var responseContent = await _openAIService.GenerateCompletionAsync(
            "gpt-3.5-turbo",
            [.. prompts], 
            temperature: 0.7, 
            maxTokens: 1000);

        var responseObject = JsonSerializer.Deserialize<ChatResponse>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        var aiResponse = (responseObject?.Choices?.Last()?.Message?.Content) 
            ?? throw new Exception("OpenAI did not return a response");
        var generatedObject = JsonSerializer.Deserialize<CronExpressionSuggestion>(aiResponse) 
            ?? throw new Exception("OpenAI did not return a response in a recognizable format.");

        Console.WriteLine($"Parsed OpenAI Response: {generatedObject.cronExpression ?? ""}");

        return generatedObject.cronExpression ?? "";
    }
}

record CronExpressionSuggestion(string cronExpression);
