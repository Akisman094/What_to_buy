namespace WhatToBuy.Identity.Configuration;

using Duende.IdentityServer.Test;

public static class AppApiTestUsers
{
    public static List<TestUser> ApiUsers =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "MasterChief",
                Password = "John117"
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "LordVader",
                Password = "LukeAndLeia"
            }
        };
}