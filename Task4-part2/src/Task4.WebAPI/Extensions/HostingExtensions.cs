using Task4.DataAccess.Data;
using Task4.DataAccess.Helpers;

namespace Task4.WebAPI.Extensions;

internal static class HostingExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<Task4Context>();
        try
        {
            await SeedDatabase.SeedAsync(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred seeding the DB.");
        }
    }
}