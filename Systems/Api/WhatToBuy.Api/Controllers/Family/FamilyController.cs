using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WhatToBuy.Api.Controllers.Family.Models;
using WhatToBuy.Common.Exceptions;
using WhatToBuy.Services.Families;

namespace WhatToBuy.Api.Controllers.Family;

// TODO: Add work with claims so users can access no families, but their own
[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Produces("application/json")]
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
    [HttpGet]
    [Authorize(Policy = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FamilyResponseDto>>> GetAll()
    {
        var families = await _familyService.GetAllAsync();
        var response = _mapper.Map<IEnumerable<FamilyResponseDto>>(families);
        return Ok(response);
    }

    /// <summary>
    /// Get a family by id.
    /// </summary>
    /// <param name="id">The id of the family to retrieve.</param>
    /// <response code="200">Returns a family.</response>
    /// <response code="400">If the user is not in a family</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FamilyResponseDto>> GetById(int id)
    {
        var family = await _familyService.GetAsync(id);

        var response = _mapper.Map<FamilyResponseDto>(family);
        return Ok(response);
    }

    /// <summary>
    /// Add a new family.
    /// </summary>
    /// <param name="familyDto">The data for the family to add.</param>
    /// <response code="200">Returns the added family.</response>
    /// <response code="400">If the user is already in a family</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FamilyResponseDto>> Create([FromBody] FamilyAddRequestDto familyDto)
    {
        var family = _mapper.Map<FamilyAddModel>(familyDto);
        var responseModel = await _familyService.CreateAsync(userId, family);
        
        var response = _mapper.Map<FamilyResponseDto>(responseModel);
        return Ok(response);
    }

    /// <summary>
    /// Update a family.
    /// </summary>
    /// <param name="id">The id of the family to update.</param>
    /// <param name="familyDto">The updated data for the family.</param>
    /// <response code="200">Returns an family.</response>
    /// <response code="400">If the user is not in a family</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] FamilyUpdateRequestDto familyDto)
    {
        var familyModel = _mapper.Map<FamilyUpdateModel>(familyDto);
        var responseModel = await _familyService.UpdateAsync(id, familyModel);
        var responseDto = _mapper.Map<FamilyResponseDto>(familyModel);
        return Ok(responseDto);
    }
}
