using FluentEmail.Core;
using MaintenanceLog.Data;
using MaintenanceLog.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceLog.Services;

public class EmailSender : IEmailSender<ApplicationUser>
{
    private readonly ILogger _logger;
    private readonly IFluentEmail _fluentEmail;

    public EmailSender(ILogger<EmailSender> logger, IFluentEmail fluentEmail)
    {
        _logger = logger;
        _fluentEmail = fluentEmail;
    }

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) => 
        SendEmailAsync(email, "Confirm your email",
            $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) => 
        SendEmailAsync(email, "Reset your password",
            $"Please reset your password using the following code: {resetCode}");

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
        SendEmailAsync(email, "Reset your password",
            $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var response = await _fluentEmail
            .To(toEmail)
            .Subject(subject)
            .Body(message, true)
            .SendAsync();

        _logger.LogInformation(response.Successful
                               ? $"Email to {toEmail} queued successfully!"
                               : $"Failure sending email to {toEmail}");
    }
} 
