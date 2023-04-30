using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using WhatToBuy.Context.Entities;
using Serilog;
using WhatToBuy.Common.Security;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;
using IdentityModel;

namespace WhatToBuy.Identity;

public class ProfileService : IProfileService
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<ProfileService> _logger;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;

    public ProfileService(
        UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IUserClaimsPrincipalFactory<User> claimsPrincipalFactory,
        ILogger<ProfileService> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _userClaimsPrincipalFactory = claimsPrincipalFactory;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        #region Standart part
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
        var claims = principal.Claims.ToList().Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();

        if(_userManager.SupportsUserRole)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);

            foreach(var role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));
            }
        }
        #endregion

        //Adding custom claims
        claims.Add(new Claim(AppClaims.FamilyIdClaim, user.FamilyId.ToString()));
        claims.Add(new Claim(AppClaims.UserNameClaim, user.UserName));

        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);

        context.IsActive = user.Status == UserStatus.Active;
    }
}
