namespace WhatToBuy.Context;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhatToBuy.Context.Entities;

public class MainDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    // TODO: Check security and then add users table
    public DbSet<User> users { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ShoppingList> ShoppingLists { get; set; }
    public DbSet<Family> Families { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("user_roles");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_role_owners");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("user_role_claims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");

        modelBuilder.Entity<Family>().ToTable("families");

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<User>().HasOne(x => x.Family).WithMany(y => y.Users).HasForeignKey(x => x.FamilyId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Item>().ToTable("items");
        modelBuilder.Entity<Item>().HasOne(x => x.ShoppingList).WithMany(y => y.Items).HasForeignKey(x => x.ShoppingListId).OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<ShoppingList>().ToTable("shoppingLists");
        modelBuilder.Entity<ShoppingList>().HasOne(x => x.Family).WithMany(x => x.ShoppingLists).HasForeignKey(x => x.FamilyId).OnDelete(DeleteBehavior.ClientCascade);
    }
}
