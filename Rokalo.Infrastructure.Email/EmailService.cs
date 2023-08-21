namespace Rokalo.Infrastructure.Email
{
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using Rokalo.Application.Contracts;
    using Rokalo.Infrastructure.Email.Configurations;
    using System;
    using System.Threading.Tasks;

    internal sealed class EmailService : IEmailService
    {
        private readonly SmtpConfiguration smtpConfig;

        public EmailService(IOptions<SmtpConfiguration> smtpConfig)
        {
            this.smtpConfig = smtpConfig.Value;
        }

        //private readonly IConfiguration configuration;

        //public EmailService(IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //}

        public async Task SendConfirmEmailAsync(string email, Guid userId, string code)
        {
            var msg = new MimeMessage();

            //msg.From.Add(MailboxAddress.Parse(configuration.GetSection("MailSettings:UserName").Value));
            msg.From.Add(MailboxAddress.Parse(this.smtpConfig.UserName));

            msg.To.Add(MailboxAddress.Parse(email));

            msg.Subject = "Confirmation Email";

            var bodyBuilder = new BodyBuilder();

            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "SendEmailConfirmation.html");

            using (StreamReader reader = File.OpenText(templatePath))
            {
                bodyBuilder.HtmlBody = reader.ReadToEnd();
            }

            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("{{user}}", email);

            // TODO read application url from settings, check url when ConfirmEmail controller is done
            bodyBuilder.HtmlBody = bodyBuilder.HtmlBody.Replace("{{link}}", $"https://localhost:7027/v1/accounts/email-confirmation/{userId}/{code}");

            msg.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(this.smtpConfig.Host, this.smtpConfig.Port, SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(this.smtpConfig.UserName, this.smtpConfig.Password);
            //await smtp.ConnectAsync(configuration.GetSection("MailSettings:Host").Value, int.Parse(configuration.GetSection("MailSettings:Port").Value), SecureSocketOptions.StartTls);

            //await smtp.AuthenticateAsync(configuration.GetSection("MailSettings:UserName").Value, configuration.GetSection("MailSettings:¸Password").Value);

            await smtp.SendAsync(msg);

            await smtp.DisconnectAsync(true);
        }
    }
}
