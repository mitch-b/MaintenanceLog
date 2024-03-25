﻿using MaintenanceLog.Common.Contracts;

namespace MaintenanceLog.Services;

public class OpenAISmartScheduleService(IOpenAIService openAIService) : ISmartScheduleService
{
    private readonly IOpenAIService _openAIService = openAIService;

    public async Task<string?> EstimateCronScheduleForItem(string? itemName, string[]? overridePrompts = null)
    {
        var prompts = overridePrompts ?? 
        [
            "You help return Cron expressions based on questions.",
            "Please return your response as just a Cron expression.",
            "Do not use any human language.",
            "Assume a person's day starts at 9AM local time.",
            $"Given typical situations, how frequently should \"{itemName}\" happen?"
        ];
        var response = await _openAIService.GenerateCompletionAsync(
            "gpt-3.5-turbo",
            [.. prompts], 
            temperature: 0.7, 
            maxTokens: 1000);
        return response;
    }
}