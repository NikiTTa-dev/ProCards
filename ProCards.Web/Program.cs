using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProCards.DAL;
using ProCards.DAL.Context;
using ProCards.DAL.Interfaces;
using ProCards.DAL.Repositories;
using ProCards.Web;
using ProCards.Web.Logic;
using Serilog;

//TODO:
// 
// Реализовать ExceptionHandler
// Настроить логирование на игнорирование запросов к Index, Create, Learn
// Реализовать ответ на ошибки
// 


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config)=>config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
builder.Services.AddDbContext(connectionString);

builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<GradesLogic>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

// app.Use(async (HttpContext context, Func<Task> next) =>
// {
//     using (var scope = app.Services.CreateScope())
//     {
//         AppDbContext dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
//         var rep = new CategoryRepository(dbContext);
//         // var a = rep.GetNineCategories(1);
//         // var b = rep.GetNineCategories(10);
//         // var c = rep.GetNineCategories(15);
//         // var rep = new CardRepository(dbContext);
//         // var a = rep.GetFiveCards("asdasd", true);
//         await next.Invoke();
//     }
// });

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        //SeedData.SeedDbData(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating DB. {ExceptionMessage}", ex.Message);
    }
}

app.Run();