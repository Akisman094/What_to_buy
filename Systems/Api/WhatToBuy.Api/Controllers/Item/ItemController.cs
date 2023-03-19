using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WhatToBuy.Api.Controllers.Models;
using WhatToBuy.Api.Services;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;
    private readonly IMapper _mapper;

    public ItemsController(IItemService itemService, IMapper mapper)
    {
        _itemService = itemService;
        _mapper = mapper;
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItemById(int id)
    {
        var item = await _itemService.GetItemAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        var itemDto = _mapper.Map<ItemResponseDto>(item);

        return Ok(itemDto);
    }

    /// <summary>
    /// Creates a new item.
    /// </summary>
    /// <param name="itemRequestDto">The data for the new item.</param>
    /// <returns>The created item.</returns>
    /// <response code="201">The created item.</response>
    /// <response code="400">The request data was invalid.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ItemResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateItem([FromBody] ItemRequestDto itemRequestDto)
    {
        var item = _mapper.Map<Item>(itemRequestDto);

        var createdItem = await _itemService.CreateItemAsync(item);

        var itemDto = _mapper.Map<ItemResponseDto>(createdItem);

        return CreatedAtAction(nameof(GetItemById), new { id = createdItem.Id }, itemDto);
    }

    /// <summary>
    /// Updates an existing item.
    /// </summary>
    /// <param name="id">The ID of the item to update.</param>
    /// <param name="itemRequestDto">The updated data for the item.</param>
    /// <returns>A response with no content.</returns>
    /// <response code="200">The item was updated successfully.</response>
    /// <response code="400">The request data was invalid.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemRequestDto itemRequestDto)
    {
        var existingItem = await _itemService.GetItemAsync(id);

        if (existingItem == null)
        {
            return NotFound();
        }

        _mapper.Map(itemRequestDto, existingItem);

        await _itemService.UpdateItemAsync(existingItem);

        return Ok();
    }

    /// <summary>
    /// Deletes a specific item by ID.
    /// </summary>
    /// <param name="id">The ID of the item to be deleted.</param>
    /// <response code="200">Item was successfully deleted.</response>
    /// <response code="404">Item with specified ID not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var item = await _itemService.GetItemAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        await _itemService.DeleteItemAsync(id);

        return Ok();
    }
}
