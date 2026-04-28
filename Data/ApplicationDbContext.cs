using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eBooking.Domain;
using eBooking.DTO;

namespace eBooking.Data
 {
 public class ApplicationDbContext : IdentityDbContext<User, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Event> Events { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<TransactionHistory> TransactionHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships and constraints if needed
        modelBuilder.Entity<Event>()
        .HasMany(e => e.Bookings)
        .WithOne(b=> b.Event)
        .HasForeignKey(b => b.EventId);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Event)
            .WithMany(e => e.Bookings)
            .HasForeignKey(b => b.EventId);

        modelBuilder.Entity<TransactionHistory>()
            .HasOne(th => th.Event)
            .WithMany()
            .HasForeignKey(th => th.EventId);

        modelBuilder.Entity<TransactionHistory>()
            .HasOne(th => th.Wallet)
            .WithMany()
            .HasForeignKey(th => th.WalletId);
    }
}
}