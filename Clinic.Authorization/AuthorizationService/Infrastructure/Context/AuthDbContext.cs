using AuthorizationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationService.Infrastructure.Context;

public class AuthDbContext:DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options){}
    public DbSet<Account> Accounts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasKey(a => a.Id);
        modelBuilder.Entity<Account>().Property(a => a.Email).IsRequired().HasMaxLength(150);
        // modelBuilder.Entity<Account>().HasIndex(a => a.Email).IsUnique();
    }
}