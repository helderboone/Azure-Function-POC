using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AzureFunctionPOC.Infra.ShoppingCartInfra
{
    public class ShoppingCartContextFactory : IDesignTimeDbContextFactory<ShoppingCartContext>
    {
        public ShoppingCartContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShoppingCartContext>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ShoppingCartConnString"));

            return new ShoppingCartContext(optionsBuilder.Options);
        }
    }
}
