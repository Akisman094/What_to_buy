using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WhatToBuy.Common.Exceptions;
using WhatToBuy.Common.Validator;
using WhatToBuy.Context.Entities;
using WhatToBuy.Services.Families;

namespace WhatToBuy.Services.Users;

public class UsersService : IUsersService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IFamilyServices _familyService;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IModelValidator<UserRegistrationModel> _userRegistrationValidator;
    private readonly IModelValidator<UserUpdateModel> _userUpdateValidator;

    public UsersService(IMapper mapper, UserManager<User> userManager, IFamilyServices familyServices, RoleManager<IdentityRole<Guid>> roleManager,
        IModelValidator<UserUpdateModel> userUpdateValidator, IModelValidator<UserRegistrationModel> userRegistrationValidator)
    {
        _mapper = mapper;
        _userManager = userManager;
        _familyService = familyServices;
        _roleManager = roleManager;
        _userUpdateValidator = userUpdateValidator;
        _userRegistrationValidator = userRegistrationValidator;
    }

    public async Task<UserModel> FindByUserNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        var userModel = _mapper.Map<UserModel>(user);
        return userModel;
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        var users = _userManager.Users.Include(x => x.Family).ToList();

        var userModels = _mapper.Map<IEnumerable<UserModel>>(users);
        return userModels;
    }

    public async Task<UserModel> RegisterUserAsync(UserRegistrationModel newUserModel)
    {
        _userRegistrationValidator.Check(newUserModel);

        var newUserFamilyName = newUserModel.Name + "'s family";
        var userPassword = newUserModel.Password;

        var familyModel = await _familyService.CreateAsync(newUserFamilyName);

        var newUser = _mapper.Map<User>(newUserModel);
        newUser.FamilyId = familyModel.Id;

        await _userManager.CreateAsync(newUser, userPassword);

        var userModel = _mapper.Map<UserModel>(newUser);

        return userModel;
    }

    public async Task AddUserRoleByUserNameAsync(string userName, string roleName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        ProcessException.ThrowIf(() => user is null, StatusCodes.Status404NotFound, $"User with username \"{userName}\" not found");

        var role = _roleManager.FindByNameAsync(roleName);
        ProcessException.ThrowIf(() => role is null, StatusCodes.Status404NotFound, $"Role with name \"{roleName}\" not found");

        await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task UpdateUserAsync(string userName, UserUpdateModel userModel)
    {
        _userUpdateValidator.Check(userModel);

        var user = await _userManager.FindByNameAsync(userName)
            ?? throw new ProcessException(StatusCodes.Status404NotFound, $"The user with userName:{userName} was not found");

        if (userModel.Name is not null)
            user.Name = userModel.Name;
        if(userModel.FamilyId is not null)
            user.FamilyId = userModel.FamilyId;

        await _userManager.UpdateAsync(user);
    }
}
