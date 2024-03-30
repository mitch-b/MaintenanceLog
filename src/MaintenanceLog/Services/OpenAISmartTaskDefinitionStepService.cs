using System.Text.Json;
using MaintenanceLog.Common.Contracts;

namespace MaintenanceLog.Services;

public class OpenAISmartTaskDefinitionStepService(IOpenAIService openAIService) : ISmartTaskDefinitionStepService
{
    private readonly IOpenAIService _openAIService = openAIService;

    public async Task<List<string>?> SuggestStepsForTaskDefinition(string? name, string? description = null, IEnumerable<string>? taskDefinitionSteps = null, IEnumerable<string>? overrideSystemPrompts = null)
    {
        var systemPrompts = overrideSystemPrompts?.ToList() ??
        [
            "You help return broken out steps to complete a Task.",
            "Please return your response in a JSON object of steps.",
            "Please keep steps concise and clear.",
            "Return no more than 5 steps.",
            $"Format should match the following example for steps to complete a task: {JsonSerializer.Serialize(new SuggestTaskDefinitionStepResponseExample(["Step 1", "Step 2"]))}",
        ];

        if (taskDefinitionSteps?.Any() == true)
        {
            systemPrompts.Add($"Do not recommend steps I already have: '{string.Join(", ", taskDefinitionSteps)}'");
        }
        var userPrompt = $"What common steps are involved in completing a task like \"{name}\" that I can check off as I complete?";
        if (!string.IsNullOrWhiteSpace(description))
        {
            userPrompt += $"{name} {description}";
        }

        var responseContent = await _openAIService.GenerateCompletionToJsonAsync(
            "gpt-3.5-turbo",
            [userPrompt],
            [.. systemPrompts],
            temperature: 0.7,
            maxTokens: 1000);

        var responseObject = JsonSerializer.Deserialize<ChatResponse>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        var aiResponse = (responseObject?.Choices?.Last()?.Message?.Content)
            ?? throw new Exception("OpenAI did not return a response");
        var generatedObject = JsonSerializer.Deserialize<SuggestTaskDefinitionStepResponseExample>(aiResponse)
            ?? throw new Exception("OpenAI did not return a response in a recognizable format.");

        return generatedObject?.steps?.ToList() ?? [];
    }
}

record SuggestTaskDefinitionStepResponseExample(string[] steps);
