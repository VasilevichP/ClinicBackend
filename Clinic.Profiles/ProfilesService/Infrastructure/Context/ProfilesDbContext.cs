using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Infrastructure.Context;

public class ProfilesDbContext: DbContext
{
    public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : base(options) { }

    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>().HasKey(p => p.Id);
        modelBuilder.Entity<Patient>().Property(p => p.FirstName).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Patient>().Property(p => p.LastName).IsRequired().HasMaxLength(100);
    }
}