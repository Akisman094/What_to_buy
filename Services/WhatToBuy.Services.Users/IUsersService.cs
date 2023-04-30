namespace WhatToBuy.Services.Users;

public interface IUsersService
{
    Task AddUserRoleByUserNameAsync(string userName, string roleName);
    Task<UserModel> FindByUserNameAsync(string userName);
    Task<IEnumerable<UserModel>> GetAllAsync();
    Task<UserModel> RegisterUserAsync(UserRegistrationModel newUserModel);
    Task UpdateUserAsync(string userName, UserUpdateModel userModel);
}