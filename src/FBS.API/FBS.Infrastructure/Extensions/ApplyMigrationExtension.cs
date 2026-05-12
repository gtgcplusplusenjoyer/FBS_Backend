using FBS.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FBS.Application.Extensions
{
    public static class ApplyMigrationExtension
    {
        public async static Task<WebApplication> ApplyMigration(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {

                var dbContext = scope.ServiceProvider.GetRequiredService<FbsDbContext>();

                try
                {
                    await dbContext.Database.MigrateAsync();
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при применении миграций: {ex.Message}");
                }

            }

            return app;
        }
    }
}
