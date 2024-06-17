using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Categories;
using Wallet.Domain.Profiles;
using Wallet.Domain.Transactions;
using Wallet.Domain.Users;

namespace Wallet.Persistence;

public sealed class WalletDbContext : DbContext
{
    public DbSet<User> Users { get; }
    public DbSet<Profile> Profiles { get; }
    public DbSet<Transaction> Transactions { get; }
    public DbSet<Category> Categories { get; }

    public WalletDbContext(DbContextOptions<WalletDbContext> options)
        : base(options)
    {
        Users = Set<User>();
        Profiles = Set<Profile>();
        Transactions = Set<Transaction>();
        Categories = Set<Category>();
    }

    public WalletDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql("Host=localhost;Database=wallet;Username=dev;Password=dev")
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<Profile>().HasKey(p => p.Id);
        modelBuilder.Entity<Profile>().HasOne<User>();

        modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
        modelBuilder.Entity<Transaction>().HasOne<Profile>();
        modelBuilder.Entity<Transaction>().HasOne<Category>();

        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        modelBuilder.Entity<Category>().HasOne<User>();
    }

    public async Task MigrateAsync()
    {
        await Database.EnsureCreatedAsync();
        await Database.MigrateAsync();
    }
}
