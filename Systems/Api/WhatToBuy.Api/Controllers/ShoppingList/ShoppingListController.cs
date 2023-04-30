namespace WhatToBuy.Api.Controllers.ShoppingList;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhatToBuy.Api.Controllers.Models;
using WhatToBuy.Api.Controllers.ShoppingList.Models;
using WhatToBuy.Common.Exceptions;
using WhatToBuy.Common.Responses;
using WhatToBuy.Common.Security;
using WhatToBuy.Common.Validator;
using WhatToBuy.EmailService;
using WhatToBuy.Services.ShoppingLists;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/shoppinglists")]
[Produces("application/json")]
[Authorize(Policy = AppScopes.Api)]
public class ShoppingListsController : ControllerBase
{
    private readonly ILogger<ShoppingListsController> _logger;
    private readonly IMapper _mapper;
    private readonly IShoppingListService _shoppingListService;
    private readonly IModelValidator<SendToEmailRequestDto> _sendToEmailModelValidator;
    private readonly IEmailSenderService _emailSenderService;

    public ShoppingListsController(ILogger<ShoppingListsController> logger, IMapper mapper, IShoppingListService shoppingListService, 
        IModelValidator<SendToEmailRequestDto> sendToEmailModelValidator, IEmailSenderService emailSenderService)
    {
        _logger = logger;
        _mapper = mapper;
        _shoppingListService = shoppingListService;
        _sendToEmailModelValidator = sendToEmailModelValidator;
        _emailSenderService = emailSenderService;
    }

    /// <summary>
    /// Retrieves all shopping lists.
    /// </summary>
    /// <response code="200">Returns the list of shopping lists.</response>
    /// <response code="400">If no Shopping lists avaliable</response>
    [HttpGet]
    [Authorize(Policy = UserRoles.Admin)]
    [ProducesResponseType(typeof(IEnumerable<ShoppingListResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetAll()
    {
        var shoppingLists = await _shoppingListService.GetAllAsync();

        var responseDto = _mapper.Map<IEnumerable<ShoppingListModel>, IEnumerable<ShoppingListResponseDto>>(shoppingLists);

        return Ok(responseDto);
    }

    /// <summary>
    /// Retrieves a specific shopping list.
    /// </summary>
    /// <param name="id">The ID of the shopping list to retrieve.</param>
    /// <response code="200">Returns the requested shopping list.</response>
    /// <response code="400">If the shopping list was not found.</response>
    /// <response code="401">If user is accessing shopping list that is not his familie's</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ShoppingListResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Get(int id)
    {
        await _shoppingListService.IsAuthorized(User, id);

        var shoppingList = await _shoppingListService.GetByIdAsync(id);
        var responseDto = _mapper.Map<ShoppingListResponseDto>(shoppingList);
        return Ok(responseDto);
    }

    /// <summary>
    /// Creates a new shopping list.
    /// </summary>
    /// <param name="request">The details of the shopping list to create.</param>
    /// <response code="200">Returns the newly created shopping list.</response>
    /// <response code="400">If the request data is invalid.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ShoppingListResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] AddShoppingListRequestDto request)
    {
        var shoppingList = _mapper.Map<AddShoppingListModel>(request);
        var createdShoppingList = await _shoppingListService.CreateAsync(User, shoppingList);

        var responseDto = _mapper.Map<ShoppingListResponseDto>(createdShoppingList);

        return Ok(responseDto);
    }

    /// <summary>
    /// Updates a shopping list with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the shopping list to update.</param>
    /// <param name="request">The update request.</param>
    /// <returns>The updated shopping list.</returns>
    /// <response code="200">Returns the updated shopping list.</response>
    /// <response code="400">If shopping list cannot be found or if request is invalid</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ShoppingListResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProcessException), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ShoppingListResponseDto>> UpdateShoppingList(int id, ShoppingListUpdateRequestDto request)
    {
        await _shoppingListService.IsAuthorized(User, id);

        var updateModel = _mapper.Map<ShoppingListUpdateModel>(request);
        var shoppingList = await _shoppingListService.UpdateAsync(id, updateModel);

        var response = _mapper.Map<ShoppingListResponseDto>(shoppingList);
        return Ok(response);
    }

    /// <summary>
    /// Deletes a shopping list with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the shopping list to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="200">The shopping list was deleted successfully.</response>
    /// <response code="400">If the shopping list could not be found.</response>
    [HttpDelete("{id}")]
    [Authorize(Policy = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProcessException), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteShoppingList(int id)
    {
        await _shoppingListService.DeleteAsync(id);

        return Ok($"Shopping list with id:{id} was deleted");
    }

    /// <summary>
    /// Sends information about shopping list to a specified email
    /// </summary>
    /// <param name="id">The ID of the shopping list to be sent.</param>
    /// <returns>Returns whether the request was passed.</returns>
    /// <response code="200">Request was passed successfully</response>
    /// <response code="400">If the shopping list could not be found.</response>
    /// <response code="401">If user is accessing shopping list that is not his family's</response>
    [HttpPost("{id}/toEmail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProcessException), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> SendToEmail(int id, SendToEmailRequestDto sendReqDto)
    {
        await _shoppingListService.IsAuthorized(User, id);

        _sendToEmailModelValidator.Check(sendReqDto);
        var destAddress = sendReqDto.EmailAddress;
        var receiverName = sendReqDto.ReceiverName;
        var body = await _shoppingListService.GetShoppingListBodyById(id, receiverName);
        
        var email = new EmailModel
        {
            DestinationAddress = destAddress,
            ReceiverName = receiverName,
            Subject = $"Shopping List from WhatToBuy",
            Body = body,
            BodyType = EmailBodyTypes.Html
        };

        await _emailSenderService.SendEmailAsync(email);

        return Ok("Request was passed successfully");
    }
}
