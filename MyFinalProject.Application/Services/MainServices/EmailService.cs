using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Application.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.MainServices
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSetting;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSetting> emailSetting ,ILogger<EmailService> logger)
        {
            _emailSetting = emailSetting.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml, CancellationToken cancellationToken)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_emailSetting.FromName, _emailSetting.FromEmail));

            message.To.Add(MailboxAddress.Parse(to));

            message.Subject = subject;

            message.Body = new TextPart(isHtml ? TextFormat.Html : TextFormat.Plain)
            {
                Text = body
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();


            var secureOption = _emailSetting.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls;

            _logger.LogInformation("Conntecting....");


            await client.ConnectAsync(_emailSetting.Host, _emailSetting.Port, secureOption, cancellationToken);

            await client.AuthenticateAsync(_emailSetting.Username, _emailSetting.Password, cancellationToken);

            await client.SendAsync(message, cancellationToken);

            await client.DisconnectAsync(true, cancellationToken);

            _logger.LogInformation("Sent");
        }
    }
}
