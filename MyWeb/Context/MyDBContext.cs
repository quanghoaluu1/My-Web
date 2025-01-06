using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Context;

public class MyDBContext: DbContext
{
    public DbSet<User> Users { get; set; }
    protected readonly IConfiguration Configuration;

    public MyDBContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=MyDB;Username=postgres;Password=12345");
    }
    
}