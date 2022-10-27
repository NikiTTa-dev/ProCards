using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProCards.Infrastructure.Models;

namespace ProCards.Infrastructure.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
    { }
    
    public DbSet<Card>? Cards { get; set; }
    public DbSet<Category>? Categories { get; set; }

    private readonly StreamWriter _logStream = new StreamWriter("logs/EFLog.txt", append: true);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(_logStream.WriteLine, LogLevel.Information);
        //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }

    public override void Dispose()
    {
        base.Dispose();
        _logStream.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await _logStream.DisposeAsync();
    }
}