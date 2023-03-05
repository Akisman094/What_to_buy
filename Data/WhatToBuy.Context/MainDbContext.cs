namespace WhatToBuy.Context;

using Microsoft.EntityFrameworkCore;
using WhatToBuy.Context.Entities;

public class MainDbContext : DbContext
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

        modelBuilder.Entity<Family>().ToTable("families");

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().Property(x => x.name).IsRequired();
        modelBuilder.Entity<User>().HasOne(x => x.Family).WithMany(y => y.Users).HasForeignKey(x => x.FamilyId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Item>().ToTable("items");
        modelBuilder.Entity<Item>().HasOne(x => x.ShoppingList).WithMany(y => y.Items).HasForeignKey(x => x.ShoppingListId).OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder.Entity<ShoppingList>().ToTable("shoppingLists");
        modelBuilder.Entity<ShoppingList>().HasOne(x => x.Family).WithMany(x => x.ShoppingLists).HasForeignKey(x => x.FamilyId).OnDelete(DeleteBehavior.ClientCascade);
    }
}
