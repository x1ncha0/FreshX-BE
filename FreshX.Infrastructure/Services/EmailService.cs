using FreshX.Application.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace FreshX.Infrastructure.Services;

public class EmailService(IConfiguration configuration) : IEmailService
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailAddress = configuration["Email:Email"] ?? throw new InvalidOperationException("Email:Email configuration is required.");
        var emailPassword = configuration["Email:Password"] ?? throw new InvalidOperationException("Email:Password configuration is required.");
        var emailHost = configuration["Email:Host"] ?? throw new InvalidOperationException("Email:Host configuration is required.");

        using var client = new SmtpClient
        {
            Credentials = new NetworkCredential
            {
                UserName = emailAddress,
                Password = emailPassword
            },
            Host = emailHost,
            Port = int.TryParse(configuration["Email:Port"], out var port) ? port : 25,
            EnableSsl = bool.TryParse(configuration["Email:EnableSsl"], out var ssl) && ssl
        };

        using var emailMessage = new MailMessage
        {
            From = new MailAddress(emailAddress),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };

        emailMessage.To.Add(new MailAddress(email));
        await client.SendMailAsync(emailMessage);
    }
}
