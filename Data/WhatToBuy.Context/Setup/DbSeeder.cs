using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WhatToBuy.Common.Security;
using WhatToBuy.Context.Entities;
using WhatToBuy.Context.Repositories;

namespace WhatToBuy.Context.Setup;

public class DbSeeder
{
    private static readonly string masterName = "Admin";
    private static readonly string masterUserEmail = "random@mail.com";
    private static readonly string masterUserPassword = "CoolAdmin";
    private static readonly IEnumerable<string> appRoles = UserRoles.getAllRoles();

    private static async Task ConfigureAdministrator(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var familyRepo = serviceProvider.GetRequiredService<IFamilyRepository>();
        var shopListRepo = serviceProvider.GetRequiredService<IShoppingListRepository>();
        var itemRepo = serviceProvider.GetRequiredService<IItemRepository>();

        await AddRoles(roleManager);

        await AddAdmin(userManager, familyRepo, shopListRepo, itemRepo);
    }

    public static void Execute(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        ConfigureAdministrator(scope.ServiceProvider).Wait();
    }

    public static async Task AddRoles(RoleManager<IdentityRole<Guid>> roleManager)
    {
        foreach (var role in appRoles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }

    public static async Task AddAdmin(UserManager<User> userManager, IFamilyRepository familyRepo, IShoppingListRepository shopListRepo, IItemRepository itemRepo)
    {
        var user = await userManager.FindByNameAsync(masterName);
        if (user == null)
        {
            var newFamily = await AddAdminFamily(familyRepo);
            var familyId = newFamily.Id;

            await AddAdminUser(userManager, familyId);
            await AddAdminToAdminRole(userManager);
            var shopList = await AddShoppingList(shopListRepo, familyId);
            var shopListId = shopList.Id;
            await AddItems(itemRepo, shopListId);
        }
    }

    public static async Task<Family> AddAdminFamily(IFamilyRepository familyRepo)
    {
        var newFamily = new Family
        {
            Name = masterName + "'s family"
        };
        await familyRepo.AddAsync(newFamily);
        return newFamily;
    }

    public static async Task<User> AddAdminUser(UserManager<User> userManager, int familyId)
    {
        var newUser = new User
        {
            Status = UserStatus.Active,
            UserName = masterName,
            Name = masterName,
            Email = masterUserEmail,
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            FamilyId = familyId,
        };

        await userManager.CreateAsync(newUser, masterUserPassword);
        return newUser;
    }

    public static async Task AddAdminToAdminRole(UserManager<User> userManager)
    {
        var user = await userManager.FindByEmailAsync(masterUserEmail);
        if (!userManager.GetRolesAsync(user).Result.Contains(UserRoles.Admin))
        {
            await userManager.AddToRoleAsync(user, UserRoles.Admin);
        }
    }

    public static async Task<ShoppingList> AddShoppingList(IShoppingListRepository shopRepo,int familyId)
    {
        var newShopList = new ShoppingList
        {
            FamilyId = familyId,
            Name = masterName + "'s list",
        };
        await shopRepo.AddAsync(newShopList);
        return newShopList;
    }

    public static async Task AddItems(IItemRepository itemRepo, int shoppingListId)
    {
        var milk = new Item
        {
            Name = "Milk 1L",
            Amount = 1,
            ShoppingListId = shoppingListId
        };
        await itemRepo.AddAsync(milk);

        var eggs = new Item
        {
            Name = "Eggs",
            Amount = 12,
            ShoppingListId = shoppingListId
        };
        await itemRepo.AddAsync(eggs);

        var iceCream = new Item
        {
            Name = "iceCream",
            Amount = 3,
            ShoppingListId = shoppingListId
        };
        await itemRepo.AddAsync(iceCream);
    }
}
