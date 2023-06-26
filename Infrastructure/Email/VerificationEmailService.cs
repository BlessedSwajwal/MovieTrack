using Application.Common.Interfaces.Service;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Email;

public class VerificationEmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfiguration;

    public VerificationEmailService(IOptions<EmailConfiguration> emailConfiguration)
    {
        _emailConfiguration = emailConfiguration.Value;
    }

    public async Task SendVerificationEmailAsync(string recipientEmail, string verificationCode)
    {
        var message = new VerificationMessage(recipientEmail, verificationCode);
        var emailMessage = CreateEmailMessage(message);
        await Send(emailMessage);
    }

    private async Task Send(MimeMessage emailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);

                client.Send(emailMessage);
            } catch
            {
                throw new Exception("Verification Email could not be sent.");
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }

    private MimeMessage CreateEmailMessage(VerificationMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Verification Sender", _emailConfiguration.From));
        emailMessage.To.Add(message.To);
        emailMessage.Subject = emailMessage.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message.Body
        };

        return emailMessage;
    }
}
