using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProCards.Infrastructure.Data;
using ProCards.Infrastructure.Data;

namespace ProCards.Infrastructure;

public static class StartupSetup
{
    public static void AddDbContext(this IServiceCollection services, string connectionString) =>
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySQL(connectionString));
}