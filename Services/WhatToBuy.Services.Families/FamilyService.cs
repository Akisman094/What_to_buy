using AutoMapper;
using Microsoft.Extensions.Logging;
using WhatToBuy.Common.Exceptions;
using WhatToBuy.Common.Validator;
using WhatToBuy.Context.Entities;
using WhatToBuy.Context.Repositories;

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

    public async Task<FamilyModel> GetAsync(int familyId)
    {
        // TODO: Get familyId from users repo
        var family = await _familyRepository.GetByIdAsync(familyId);
        ProcessException.ThrowIf(() => family is null, $"Family with id {familyId} not found.");

        return _mapper.Map<FamilyModel>(family);
    }

    public async Task<FamilyModel> CreateAsync(int userId)
    {
        var family = new Family();
        await _familyRepository.AddAsync(family);

        return _mapper.Map<FamilyModel>(family);
    }

    public async Task<FamilyModel> UpdateAsync(int id, FamilyUpdateModel familyDto)
    {
        var existingFamily = await _familyRepository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => existingFamily is null, $"Family with id {id} not found.");

        _mapper.Map(familyDto, existingFamily);
        await _familyRepository.UpdateAsync(existingFamily);

        return _mapper.Map<FamilyModel>(existingFamily);
    }

    public async Task<IEnumerable<FamilyModel>> GetAllAsync()
    {
        var families = await _familyRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<FamilyModel>>(families);
    }
}

