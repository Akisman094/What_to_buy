namespace WhatToBuy.Common.Security;

public static class UserRoles
{
    public const string Admin = "Admin";

    public static IEnumerable<string> getAllRoles()
    {
        var roles = new List<string>
        {
            Admin
        };

        return roles;
    }
}
