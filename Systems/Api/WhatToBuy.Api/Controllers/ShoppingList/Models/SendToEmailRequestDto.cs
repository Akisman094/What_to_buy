using AutoMapper;
using FluentValidation;
using WhatToBuy.Common;

namespace WhatToBuy.Api.Controllers.ShoppingList.Models;

public class SendToEmailRequestDto
{
    public string EmailAddress { get; set; }
    public string ReceiverName { get; set; }
}

public class SendToEmailRequestDtoValidator : AbstractValidator<SendToEmailRequestDto>
{
    public SendToEmailRequestDtoValidator()
    {
        RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email address cannot be empty.")
            .Must(x => x.IsValidEmail()).WithMessage("Invalid email address.");
        RuleFor(x => x.ReceiverName).NotEmpty().WithMessage("Receiver name cannot be empty.");
    }
}