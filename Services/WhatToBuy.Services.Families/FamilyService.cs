using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using WhatToBuy.Common.Exceptions;
using WhatToBuy.Common.Security;
using WhatToBuy.Context.Entities;
using WhatToBuy.Context.Repositories;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;

namespace WhatToBuy.Services.Families;
public class FamilyServices : IFamilyServices
{
    private readonly IFamilyRepository _familyRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<FamilyServices> _logger;

    public FamilyServices(IFamilyRepository repository, IMapper mapper, ILogger<FamilyServices> logger)
    {
        _familyRepository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<FamilyModel> GetByIdAsync(int familyId)
    {
        var family = await _familyRepository.GetByIdAsync(familyId);
        ProcessException.ThrowIf(() => family is null, $"Family with id {familyId} not found.");

        return _mapper.Map<FamilyModel>(family);
    }

    public async Task<FamilyModel> GetUsersFamilyAsync(ClaimsPrincipal user)
    {
        var familyId = int.Parse(user.FindFirstValue(AppClaims.FamilyIdClaim));
        var family = await GetByIdAsync(familyId);
        return family;
    }

    public async Task<IEnumerable<FamilyModel>> GetAllAsync()
    {
        var families = await _familyRepository.GetAllAsync();
        var response = _mapper.Map<IEnumerable<Family>, IEnumerable<FamilyModel>>(families);
        return response;
    }

    public async Task<FamilyModel> CreateAsync(string familyName)
    {
        var family = new Family(familyName);
        await _familyRepository.AddAsync(family);

        var familyModel = _mapper.Map<FamilyModel>(family);
        return familyModel;
    }

    public async Task<FamilyModel> UpdateAsync(int id, FamilyUpdateModel familyDto)
    {
        var existingFamily = await _familyRepository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => existingFamily is null, $"Family with id {id} not found.");

        _mapper.Map(familyDto, existingFamily);
        await _familyRepository.UpdateAsync(existingFamily);

        return _mapper.Map<FamilyModel>(existingFamily);
    }

    public async Task<bool> IsAuthorized(ClaimsPrincipal user, int familyId)
    {
        var tokenFamilyId = int.Parse(user.FindFirstValue(AppClaims.FamilyIdClaim));

        ProcessException.ThrowIf(() => tokenFamilyId != familyId, StatusCodes.Status401Unauthorized, $"Can't access family with id:{familyId}, because user is not in it.");

        return true;
    }
}

