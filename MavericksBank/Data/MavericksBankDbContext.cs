using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Data
{
    public class MavericksBankDbContext : DbContext
    {
        public MavericksBankDbContext(DbContextOptions<MavericksBankDbContext> options) : base(options) 
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Loan > Loans { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.AccountNumber)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Customer)    
                .WithOne(c => c.User)
                .HasForeignKey<Customer>(c => c.UserId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.FromAccount)
                .WithMany(a => a.SentTransactions)
                .HasForeignKey(t => t.FromAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.ToAccount)
                .WithMany(a => a.ReceivedTransactions)
                .HasForeignKey(t => t.ToAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
