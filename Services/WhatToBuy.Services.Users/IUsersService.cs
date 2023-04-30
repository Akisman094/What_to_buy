namespace WhatToBuy.Services.Users;

public interface IUsersService
{
    Task AddUserRoleByUserNameAsync(string userName, string roleName);
    Task<UserModel> FindByIdAsync(string uid);
    Task<UserModel> FindByUserNameAsync(string userName);
    Task<string> GeneratePasswordResetEmailBodyAsync(string callbackUrl, string name);
    Task<string> GeneratePasswordResetTokenAsync(UserModel userModel);
    Task<IEnumerable<UserModel>> GetAllAsync();
    Task<UserModel> RegisterUserAsync(UserRegistrationModel newUserModel);
    Task ResetPasswordAsync(UserModel userModel, string resetToken, string newPassword);
    Task UpdateUserAsync(string userName, UserUpdateModel userModel);
}