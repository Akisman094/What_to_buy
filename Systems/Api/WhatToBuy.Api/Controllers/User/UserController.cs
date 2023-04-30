using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WhatToBuy.Api.Controllers.Models;
using WhatToBuy.Api.Controllers.ShoppingList;
using WhatToBuy.Common.Responses;
using WhatToBuy.Common.Security;
using WhatToBuy.EmailService;
using WhatToBuy.Services.Users;

namespace WhatToBuy.Api.Controllers.User;

/// <summary>
/// Controller for user management endpoints
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
[Authorize(Policy = AppScopes.Api)]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly ILogger<ShoppingListsController> _logger;
    private readonly IMapper _mapper;
    private readonly IUsersService _userService;
    private readonly IEmailSenderService _emailSenderService;

    public UserController(ILogger<ShoppingListsController> logger, IMapper mapper, IUsersService shoppingListService, IEmailSenderService emailSenderService)
    {
        _logger = logger;
        _mapper = mapper;
        _userService = shoppingListService;
        _emailSenderService = emailSenderService;
    }

    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <param name="registrationRequest">The user registration information</param>
    /// <returns>A success message if the registration was successful</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto registrationRequest)
    {
        var userRegistrationModel = _mapper.Map<UserRegistrationModel>(registrationRequest);

        var newUserModel = await _userService.RegisterUserAsync(userRegistrationModel);
        var response = _mapper.Map<UserResponseDto>(newUserModel);

        return Ok(response);
    }

    /// <summary>
    /// Adds a user to a role.
    /// </summary>
    /// <returns>A response indicating whether the user was successfully added to the role.</returns>
    [HttpPost("roles")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddUserToRole(UserAddToRoleDto userAddToRoleDto)
    {
        var userName = userAddToRoleDto.UserName;
        var role = userAddToRoleDto.Role;

        await _userService.AddUserRoleByUserNameAsync(userName, role);

        return Ok($"User '{userName}' successfully added to role '{role}'.");
    }

    /// <summary>
    /// Get details of a user.
    /// </summary>
    /// <response code="200">Returns a user details.</response>
    /// <response code="400">Returns an error response if the request is invalid.</response>
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get()
    {
        var userName = User.FindFirstValue(AppClaims.UserNameClaim);

        var userModel = await _userService.FindByUserNameAsync(userName);
        var response = _mapper.Map<UserResponseDto>(userModel);
        return Ok(response);
    }

    /// <summary>
    /// Get details of any user.
    /// </summary>
    /// <response code="200">Returns a user details.</response>
    /// <response code="400">Returns an error response if the request is invalid.</response>
    [HttpGet("all")]
    [Authorize(Policy = UserRoles.Admin)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var userModel = await _userService.GetAllAsync();
        var response = _mapper.Map<IEnumerable<UserResponseDto>>(userModel);
        return Ok(response);
    }

    /// <summary>
    /// Updates the name or family ID of a user.
    /// </summary>
    /// <response code="200">Returns a success message if the update was successful.</response>
    /// <response code="400">Returns an error response if the request is invalid or the update failed.</response>
    [HttpPut]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(UserUpdateRequestDto updateRequest)
    {
        var userName = User.FindFirstValue(AppClaims.UserNameClaim);
        var updateModel = _mapper.Map<UserUpdateModel>(updateRequest);

        await _userService.UpdateUserAsync(userName, updateModel);

        return Ok("User updated successfully.");
    }

    /// <summary>
    /// Updates the name or family ID of any user.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="request">The request containing the new name and/or family ID.</param>
    /// <response code="200">Returns a success message if the update was successful.</response>
    /// <response code="400">Returns an error response if the request is invalid or the update failed.</response>
    [HttpPut("{userName}")]
    [Authorize(Policy = UserRoles.Admin)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(string userName, UserUpdateRequestDto updateRequest)
    {
        var userModel = _mapper.Map<UserUpdateModel>(updateRequest);

        await _userService.UpdateUserAsync(userName, userModel);

        return Ok("User updated successfully.");
    }

    /// <summary>
    /// Initiates a reset password sequence.
    /// </summary>
    /// <param name="request">UserName of a user that wants to reset his password.</param>
    /// <response code="200">Returns a success message reset email was successfully passed.</response>
    /// <response code="400">Returns an error response if the user is not found.</response>
    [HttpPost("reset-password")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto request)
    {
        // Find user by username address
        var user = await _userService.FindByUserNameAsync(request.UserName);

        // Generate password reset token
        var token = await _userService.GeneratePasswordResetTokenAsync(user);

        // Generate body and callbackUrl
        var callbackUrl = Url.Action("ResetPassword", "User", new { userId = user.Id, token }, Request.Scheme);
        var body = await _userService.GeneratePasswordResetEmailBodyAsync(callbackUrl, user.Name);

        //Generate EmailModel
        var email = new EmailModel
        {
            DestinationAddress = user.Email,
            ReceiverName = user.Name,
            Subject = "Password reset",
            Body = body,
            BodyType = EmailBodyTypes.Html
        };

        //Send email
        await _emailSenderService.SendEmailAsync(email);

        return Ok("Email sent successfully");
    }

    /// <summary>
    /// Reset password from the link
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="token">Token</param>
    /// <param name="model">New password</param>
    /// <returns></returns>
    [HttpPost("reset-password/{userId}/{token}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword(string userId, string token, [FromBody] NewPasswordDto model)
    {
        // Find user by ID
        var userModel = await _userService.FindByIdAsync(userId);

        //Decoding special symbols
        token = System.Web.HttpUtility.UrlDecode(token);
        
        //fixing issue: + for some reason is decoded as space
        token = token.Replace(' ', '+');

        // Reset password
        await _userService.ResetPasswordAsync(userModel, token, model.NewPassword);

        return Ok("Password was reset successfully");
    }

}
