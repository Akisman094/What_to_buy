﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhatToBuy.Api.Controllers.Family.Models;
using WhatToBuy.Common.Security;
using WhatToBuy.Services.Families;

namespace WhatToBuy.Api.Controllers.Family;

// TODO: Add work with claims so users can access no families, but their own
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Authorize(Policy = AppScopes.Api)]
public class FamilyController : ControllerBase
{
    private readonly IFamilyServices _familyService;
    private readonly IMapper _mapper;
    private readonly ILogger<FamilyController> _logger;

    public FamilyController(IFamilyServices familyService, IMapper mapper, ILogger<FamilyController> logger)
    {
        _familyService = familyService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Get all families.
    /// </summary>
    /// <response code="200">A list of all families.</response>
    /// <response code="400">If no families avaliable</response>
    /// <response code="401">If user is not admin</response>
    [HttpGet("all")]
    [Authorize(Policy = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<FamilyResponseDto>>> GetAll()
    {
        var families = await _familyService.GetAllAsync();
        var response = _mapper.Map<IEnumerable<FamilyResponseDto>>(families);
        return Ok(response);
    }

    /// <summary>
    /// Get Users own family.
    /// </summary>
    /// <response code="200">Returns a family.</response>
    /// <response code="400">If the user is not in a family</response>
    /// <response code="401">User is trying to access family, while he is not in it</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<FamilyResponseDto>> GetById()
    {

        var family = await _familyService.GetUsersFamilyAsync(User);

        var response = _mapper.Map<FamilyResponseDto>(family);
        return Ok(response);
    }

    /// <summary>
    /// Change family name.
    /// </summary>
    /// <param name="id">The id of the family to update.</param>
    /// <param name="familyDto">The updated data for the family.</param>
    /// <response code="200">Returns a family.</response>
    /// <response code="400">If the user is not in a family</response>
    /// <response code="401">User is trying to modify family, while he is not in it</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(int id, [FromBody] FamilyUpdateRequestDto familyDto)
    {
        await _familyService.IsAuthorized(User, id);

        var familyModel = _mapper.Map<FamilyUpdateModel>(familyDto);
        var responseModel = await _familyService.UpdateAsync(id, familyModel);
        var responseDto = _mapper.Map<FamilyResponseDto>(responseModel);
        return Ok(responseDto);
    }
}
