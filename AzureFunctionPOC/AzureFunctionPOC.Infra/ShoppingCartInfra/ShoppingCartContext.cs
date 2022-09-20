using AzureFunctionPOC.Domain.ShoppingCart;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;

namespace AzureFunctionPOC.Infra.ShoppingCartInfra;

public class ShoppingCartContext : DbContext
{
    public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
            : base(options)
    {
    }

    /// <summary>
    /// Display output query sql
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }

    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<ShoppingCart> ShoppingCart { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
