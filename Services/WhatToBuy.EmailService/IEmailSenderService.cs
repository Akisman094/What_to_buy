namespace WhatToBuy.EmailService;

public interface IEmailSenderService
{
    Task SendEmailAsync(string destAddress, string receiverName, string body);
}