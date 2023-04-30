using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace WhatToBuy.EmailService;
public class EmailSenderService : IEmailSenderService
{
    private readonly EmailSenderSettings _settings;
    private const string subject = "Shopping List";

    public EmailSenderService(EmailSenderSettings settings)
    {
        _settings = settings;
    }

    public async Task SendEmailAsync(string destAddress, string receiverName, string body)
    {
        // create message
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("WhatToBuy", _settings.EmailAddress));
        message.To.Add(new MailboxAddress(receiverName, destAddress));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        // configure client
        using var client = new SmtpClient();
        await client.ConnectAsync(_settings.SmtpServerUrl, _settings.SmtpPort, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_settings.EmailAddress, _settings.EmailPassword);

        // send message
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
