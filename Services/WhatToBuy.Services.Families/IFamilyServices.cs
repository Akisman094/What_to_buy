namespace WhatToBuy.Services.Families;

public interface IFamilyServices
{
    Task<FamilyModel> CreateAsync(int userId);
    Task<IEnumerable<FamilyModel>> GetAllAsync();
    Task<FamilyModel> GetAsync(int userId);
    Task<FamilyModel> UpdateAsync(int id, FamilyUpdateModel familyDto);
}