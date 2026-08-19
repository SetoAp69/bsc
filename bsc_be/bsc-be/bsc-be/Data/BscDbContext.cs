using Microsoft.EntityFrameworkCore;

namespace bsc_be.Models
{
    public class BscDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<GigType> GigTypes { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Add relations here
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique(true);
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique(true);
            modelBuilder.Entity<Transaction>()
            .HasOne<Rating>(t => t.Rating);
            modelBuilder.Entity<Transaction>()
            .HasOne<PaymentMethod>(t => t.PaymentMethod);
            modelBuilder.Entity<Transaction>()
            .HasOne<Item>(t => t.Item);
            modelBuilder.Entity<Transaction>()
            .HasOne<Gig>(t => t.Gig)
            .WithMany(g => g.Transactions)
            .HasForeignKey(t => t.GigId);

        }
    }

}