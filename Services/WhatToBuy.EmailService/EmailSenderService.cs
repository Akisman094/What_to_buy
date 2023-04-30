using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace WhatToBuy.EmailService;
public class EmailSenderService : IEmailSenderService
{
    private readonly EmailSenderSettings _settings;

    public EmailSenderService(EmailSenderSettings settings)
    {
        _settings = settings;
    }


    /// <summary>
    /// Method to send emails
    /// </summary>
    /// <param name="email">Email model</param>
    /// <param name="type">Either "plain" or "html"</param>
    /// <returns></returns>
    public async Task SendEmailAsync(EmailModel email)
    {
        // create message
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("WhatToBuy", _settings.EmailAddress));
        message.To.Add(new MailboxAddress(email.ReceiverName, email.DestinationAddress));
        message.Subject = email.Subject;
        message.Body = new TextPart(email.BodyType) { Text = email.Body };

        // configure client
        using var client = new SmtpClient();
        await client.ConnectAsync(_settings.SmtpServerUrl, _settings.SmtpPort, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_settings.EmailAddress, _settings.EmailPassword);

        // send message
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
