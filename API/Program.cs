using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host=CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory= services.GetRequiredService<ILoggerFactory>();
                
                try
                {
                    var storeContext = services.GetRequiredService<StoreContext>();
                    var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();

                    await storeContext.Database.MigrateAsync();
                    await identityContext.Database.MigrateAsync();

                    await StoreContextSeed.seedAsync(storeContext, loggerFactory);
                    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);

                }
                catch (Exception exception)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(exception, "An error ocurred during migration");
                }
                host.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
