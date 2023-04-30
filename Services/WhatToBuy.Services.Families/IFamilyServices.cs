using System.Security.Claims;

namespace WhatToBuy.Services.Families;
public interface IFamilyServices
{
    Task<FamilyModel> CreateAsync(string familyName);
    Task<IEnumerable<FamilyModel>> GetAllAsync();
    Task<FamilyModel> GetByIdAsync(int familyId);
    Task<FamilyModel> GetUsersFamilyAsync(ClaimsPrincipal user);
    Task<bool> IsAuthorized(ClaimsPrincipal user, int familyId);
    Task<FamilyModel> UpdateAsync(int id, FamilyUpdateModel familyDto);
}