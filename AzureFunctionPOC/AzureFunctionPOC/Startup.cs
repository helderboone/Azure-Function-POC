using AzureFunctionPOC.Infra.ShoppingCartInfra;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(AzureFunctionPOC.Startup))]
namespace AzureFunctionPOC;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = builder.GetContext().Configuration;

        string connectionString = Environment.GetEnvironmentVariable("ShoppingCartConnString");
        builder.Services.AddDbContext<ShoppingCartContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));

    }
}
