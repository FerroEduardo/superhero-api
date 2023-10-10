using IntegrationTests.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SuperHeroAPI.Data;

namespace IntegrationTests
{
    internal class SuperHeroWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<DataContext>));
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseSqlServer(getDatabaseUrl());
                });

                var dbContext = CreateDbContext(services);
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                DatabaseUtil.Seed(dbContext);
            });
        }

        private static string getDatabaseUrl()
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("testing.json", optional: false)
                .Build();

            var url = config["Database:URL"]!;

            return url;
        }

        private static DataContext CreateDbContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            return dbContext;
        }
    }
}
