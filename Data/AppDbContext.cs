using ApiAlertaDengue.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAlertaDengue.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }

    public DbSet<DengueAlert> DengueAlerts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DengueAlert>()
            .HasIndex(da => new {da.EpidemologicalWeek,da.Year,da.EstimatedCases})
            .IsUnique();
    }
}