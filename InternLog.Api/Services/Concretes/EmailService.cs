using FluentEmail.Core;
using FluentEmail.Smtp;
using InternLog.Api.Services.Contracts;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
namespace InternLog.Api.Services.Concretes
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var sender = new SmtpSender(new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
            });

            Email.DefaultSender = sender;
            EmailTemplate template = new();

            var sentEmail = await Email.From("system@internlog.com")
                .To(email)
                .Subject(subject)
                .UsingTemplate(htmlMessage, template)
                .SendAsync();
        }
    }

    public class EmailTemplate { }
}
