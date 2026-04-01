using Ecommerce.Core.DTOS;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace EcommerceInfraStructure.Repositries.Service
{
    public class EmailService : IEmailService
    {
            private readonly IConfiguration configuration;
            public EmailService(IConfiguration configuration)
            {
                this.configuration = configuration;
            }
            public async Task SendEmail(EmailDTO emailDTO)
            {
                MimeMessage message = new();
                message.From.Add(new MailboxAddress("Fadell Ecommerce", configuration["EmailSetting:From"]));
                message.Subject = emailDTO.Subject;
                message.To.Add(new MailboxAddress(emailDTO.To, emailDTO.To));
                message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = emailDTO.Content
                };
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    try
                    {
                        await smtp.ConnectAsync(
                            configuration["EmailSetting:Smtp"],
                           int.Parse(configuration["EmailSetting:Port"]), true);
                        await smtp.AuthenticateAsync(configuration["EmailSetting:Username"],
                            configuration["EmailSetting:Password"]);

                        await smtp.SendAsync(message);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    finally
                    {
                        smtp.Disconnect(true);
                        smtp.Dispose();
                    }
                }
            }
        }
}
