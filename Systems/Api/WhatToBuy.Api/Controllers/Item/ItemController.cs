using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WhatToBuy.Api.Controllers.Models;
using WhatToBuy.Common.Responses;
using WhatToBuy.Services.Items;

namespace WhatToBuy.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;
    private readonly IMapper _mapper;
    private readonly ILogger<ItemController> _logger;

    public ItemController(IItemService itemService, IMapper mapper, ILogger<ItemController> logger)
    {
        _itemService = itemService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Gets all items.
    /// </summary>
    /// <param name="id">The ID of the item to get.</param>
    /// <returns>The requested item.</returns>
    /// <response code="200">The requested item.</response>
    /// <response code="400">No items avaliable.</response>
    [HttpGet("{id}")]
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
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetItemById(int id)
    {
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
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ItemResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> CreateItem([FromBody] ItemAddRequestDto itemRequestDto)
    {
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
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemAddRequestDto itemRequestDto)
    {
        var updateModel = _mapper.Map<ItemUpdateModel>(itemRequestDto);
        await _itemService.UpdateItemAsync(id, updateModel);

        return Ok();
    }

    /// <summary>
    /// Deletes a specific item by ID.
    /// </summary>
    /// <param name="id">The ID of the item to be deleted.</param>
    /// <response code="200">Item was successfully deleted.</response>
    /// <response code="400">Item with specified ID not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<ActionResult> Delete(int id)
    {
        await _itemService.DeleteItemAsync(id);

        return Ok();
    }
}
