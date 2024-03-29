﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProCards.DAL.Context;

namespace ProCards.DAL;

public static class StartupSetup
{
    public static void AddDbContext(this IServiceCollection services, string connectionString) =>
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString).EnableSensitiveDataLogging()); //use for sqlite connection
            //options.UseNpgsql(connectionString).EnableSensitiveDataLogging());
}