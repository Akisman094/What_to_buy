namespace WhatToBuy.EmailService;

public class EmailModel
{
    public string DestinationAddress { get; set; }
    public string ReceiverName { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string BodyType { get; set; }

}
