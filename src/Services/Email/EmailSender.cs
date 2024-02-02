using Microsoft.AspNetCore.Identity.UI.Services;

using System.Net;
using System.Net.Mail;

namespace Services.Email;

public class EmailSender(ElasticEmailCredentials elasticCredentials) : IEmailSender
{
    private readonly ElasticEmailCredentials _elasticCredentials = elasticCredentials;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        using SmtpClient client = new(_elasticCredentials.Smtp.Server);

        client.Port = int.Parse(_elasticCredentials.Smtp.Port);
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(_elasticCredentials.Smtp.Username, _elasticCredentials.Smtp.Password);

        MailMessage mailMessage = new()
        {
            From = new MailAddress(_elasticCredentials.Smtp.From, _elasticCredentials.Smtp.DisplayName),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await client.SendMailAsync(mailMessage);
    }
}
