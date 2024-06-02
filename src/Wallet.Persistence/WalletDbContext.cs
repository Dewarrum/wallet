using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Categories;
using Wallet.Domain.Profiles;
using Wallet.Domain.Transactions;
using Wallet.Domain.Users;

namespace Wallet.Persistence;

internal sealed class WalletDbContext : DbContext
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);

        modelBuilder.Entity<Profile>().HasKey(p => p.Id);

        modelBuilder.Entity<Transaction>().HasKey(t => t.Id);

        modelBuilder.Entity<Category>().HasKey(c => c.Id);
    }
}
