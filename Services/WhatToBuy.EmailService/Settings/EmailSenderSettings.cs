namespace WhatToBuy.EmailService;
public class EmailSenderSettings
{
    public string SmtpServerUrl { get; private set; }
    public int SmtpPort { get; set; }
    public string EmailAddress { get; private set; }
    public string EmailPassword { get; private set; }
}
