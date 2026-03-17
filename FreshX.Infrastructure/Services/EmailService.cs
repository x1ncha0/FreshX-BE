using FreshX.Application.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace FreshX.Infrastructure.Services;

public class EmailService(IConfiguration configuration) : IEmailService
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        using var client = new SmtpClient
        {
            Credentials = new NetworkCredential
            {
                UserName = configuration["Email:Email"],
                Password = configuration["Email:Password"]
            },
            Host = configuration["Email:Host"],
            Port = int.TryParse(configuration["Email:Port"], out var port) ? port : 25,
            EnableSsl = bool.TryParse(configuration["Email:EnableSsl"], out var ssl) && ssl
        };

        using var emailMessage = new MailMessage
        {
            From = new MailAddress(configuration["Email:Email"] ?? "noreply@freshx.local"),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };

        emailMessage.To.Add(new MailAddress(email));
        await client.SendMailAsync(emailMessage);
    }
}