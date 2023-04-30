using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhatToBuy.Api.Controllers.Models;
using WhatToBuy.Common.Responses;
using WhatToBuy.Common.Security;
using WhatToBuy.Services.Items;
using WhatToBuy.Services.ShoppingLists;

namespace WhatToBuy.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Produces("application/json")]
[Authorize(Policy = AppScopes.Api)]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;
    private readonly IShoppingListService _shoppingListService;
    private readonly IMapper _mapper;
    private readonly ILogger<ItemController> _logger;

    public ItemController(IItemService itemService, IMapper mapper, ILogger<ItemController> logger, IShoppingListService shoppingListService)
    {
        _itemService = itemService;
        _mapper = mapper;
        _logger = logger;
        _shoppingListService = shoppingListService;
    }

    /// <summary>
    /// Gets all items.
    /// </summary>
    /// <returns>The requested item.</returns>
    /// <response code="200">The requested item.</response>
    /// <response code="400">No items avaliable.</response>
    [HttpGet]
    [Authorize(Policy = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetItems()
    {
        var items = await _itemService.GetAllItemsAsync();
        var itemDto = _mapper.Map<List<ItemResponseDto>>(items);

        return Ok(itemDto);
    }

    /// <summary>
    /// Gets an item by ID.
    /// </summary>
    /// <param name="id">The ID of the item to get.</param>
    /// <returns>The requested item.</returns>
    /// <response code="200">The requested item.</response>
    /// <response code="404">The item with the specified ID was not found.</response>
    /// <response code="401">User is trying to access item that he is not authorized to access</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetItemById(int id)
    {
        await _itemService.IsAuthorized(User, id);

        var item = await _itemService.GetByIdAsync(id);
        var itemDto = _mapper.Map<ItemResponseDto>(item);

        return Ok(itemDto);
    }

    /// <summary>
    /// Creates a new item.
    /// </summary>
    /// <param name="itemRequestDto">The data for the new item.</param>
    /// <returns>The created item.</returns>
    /// <response code="200">The created item.</response>
    /// <response code="400">The request data was invalid.</response>
    /// <response code="401">User is trying to access item that he is not authorized to access</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> CreateItem([FromBody] ItemAddRequestDto itemRequestDto)
    {
        await _shoppingListService.IsAuthorized(User, itemRequestDto.ShoppingListId);

        var item = _mapper.Map<ItemAddModel>(itemRequestDto);

        var createdItem = await _itemService.CreateItemAsync(item);

        var itemDto = _mapper.Map<ItemResponseDto>(createdItem);

        return Ok(itemDto);
    }

    /// <summary>
    /// Updates an existing item.
    /// </summary>
    /// <param name="id">The ID of the item to update.</param>
    /// <param name="itemRequestDto">The updated data for the item.</param>
    /// <returns>A response with no content.</returns>
    /// <response code="200">The item was updated successfully.</response>
    /// <response code="400">The request data was invalid or item with specified id not found.</response>
    /// <response code="401">User is trying to access item that he is not authorized to access</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemUpdateRequestDto itemRequestDto)
    {
        await _itemService.IsAuthorized(User, id);

        var updateModel = _mapper.Map<ItemUpdateModel>(itemRequestDto);
        await _itemService.UpdateItemAsync(id, updateModel);

        return Ok($"Item with id:{id} was updated successfully");
    }

    /// <summary>
    /// Deletes a specific item by ID.
    /// </summary>
    /// <param name="id">The ID of the item to be deleted.</param>
    /// <response code="200">Item was successfully deleted.</response>
    /// <response code="400">Item with specified ID not found.</response>
    /// <response code="401">User is trying to access item that he is not authorized to access</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> Delete(int id)
    {
        await _itemService.IsAuthorized(User, id);

        await _itemService.DeleteItemAsync(id);

        return Ok($"Item with id:{id} was deleted successfully");
    }
}
