using FoiaOnline.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FoiaOnline.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<FoundRequest> FoundRequests { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoundRequest>().HasKey(x => x.TrackingNumber);
    }
}