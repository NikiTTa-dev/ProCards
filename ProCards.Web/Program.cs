using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProCards.Core.Data.Repositories;
using ProCards.DAL;
using ProCards.DAL.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.Use(async (HttpContext context, Func<Task> next) =>
{
    using (var scope = app.Services.CreateScope())
    {
        AppDbContext dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
        var rep = new CategoryRepository(dbContext);
        // var a = rep.GetNineCategories(1);
        // var b = rep.GetNineCategories(10);
        // var c = rep.GetNineCategories(15);
        // var rep = new CardRepository(dbContext);
        // var a = rep.GetFiveCards("asdasd", true);
        await next.Invoke();
    }
});

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating DB. {ExceptionMessage}", ex.Message);
    }
}

app.Run();