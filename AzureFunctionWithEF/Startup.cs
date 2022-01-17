using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using AzureFunctionWithEF.Common.Data;
using AzureFunctionWithEF.Services;
using AzureFunctionWithEF.Repositories;

[assembly: FunctionsStartup(typeof(AzureFunctionWithEF.StartUp))]

namespace AzureFunctionWithEF
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.AddDbContext<MyDbContext>(
              options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));

            builder.Services.AddScoped<IDimAccountRepository, DimAccountRepository>();
            builder.Services.AddScoped<IDimService, DimService>();

        }
    }
}