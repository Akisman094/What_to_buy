namespace WhatToBuy.EmailService;

public interface IEmailSenderService
{
    Task SendEmailAsync(EmailModel email);
}