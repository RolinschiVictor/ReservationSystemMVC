using Microsoft.EntityFrameworkCore;
using ReservationSystemMVC.Core.Domain.Entities;

namespace ReservationSystemMVC.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasMaxLength(256)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.PasswordHash)
            .HasMaxLength(512)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasMaxLength(32)
            .IsRequired();

        modelBuilder.Entity<Booking>()
            .Property(b => b.UserEmail)
            .HasMaxLength(256)
            .IsRequired();

        modelBuilder.Entity<Booking>()
            .Property(b => b.UserFullName)
            .HasMaxLength(256);

        modelBuilder.Entity<Booking>()
            .Property(b => b.PhoneNumber)
            .HasMaxLength(64);

        modelBuilder.Entity<Payment>()
            .HasIndex(p => p.StripeSessionId)
            .IsUnique();
    }
}
