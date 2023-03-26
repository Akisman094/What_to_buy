namespace WhatToBuy.Api.Controllers.ShoppingList;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WhatToBuy.Api.Controllers.Models;
using WhatToBuy.Common.Exceptions;
using WhatToBuy.Common.Responses;
using WhatToBuy.Services.ShoppingLists;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/shoppinglists")]
[Produces("application/json")]
public class ShoppingListsController : ControllerBase
{
    private readonly ILogger<ShoppingListsController> _logger;
    private readonly IMapper _mapper;
    private readonly ShoppingListService _shoppingListService;

    public ShoppingListsController(ILogger<ShoppingListsController> logger, IMapper mapper, ShoppingListService shoppingListService)
    {
        _logger = logger;
        _mapper = mapper;
        _shoppingListService = shoppingListService;
    }

    /// <summary>
    /// Retrieves all shopping lists.
    /// </summary>
    /// <response code="200">Returns the list of shopping lists.</response>
    /// <response code="400">If no Shopping lists avaliable</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ShoppingListResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetAll()
    {
        var shoppingLists = await _shoppingListService.GetAllAsync();

        var responseDto = _mapper.Map<List<ShoppingListResponseDto>>(shoppingLists);

        return Ok(responseDto);
    }

    /// <summary>
    /// Retrieves a specific shopping list.
    /// </summary>
    /// <param name="id">The ID of the shopping list to retrieve.</param>
    /// <response code="200">Returns the requested shopping list.</response>
    /// <response code="400">If the shopping list was not found.</response>
    [HttpGet("{id:int}", Name = nameof(Get))]
    [ProducesResponseType(typeof(ShoppingListResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var shoppingList = await _shoppingListService.GetByIdAsync(id);

        return Ok(_mapper.Map<ShoppingListResponseDto>(shoppingList));
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
        var createdShoppingList = await _shoppingListService.CreateAsync(shoppingList);

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProcessException), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteShoppingList(int id)
    {
        await _shoppingListService.DeleteAsync(id);

        return Ok();
    }
}
