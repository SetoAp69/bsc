using Microsoft.EntityFrameworkCore;

namespace bsc_be.Models
{
    public class BscDbContext : DbContext
    {
        public BscDbContext(DbContextOptions<BscDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<Gig> Gigs { get; set; }
        public DbSet<GigType> GigTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Item> Items { get; set; }

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
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Password)
                .IsUnique(true);
            modelBuilder.Entity<Gig>()
                .HasOne(g => g.User)
                .WithMany(u => u.Gigs)
                .HasForeignKey(g => g.UserId);
            modelBuilder.Entity<GigType>()
                .HasOne(gt => gt.Gig)
                .WithMany()
                .HasForeignKey(gt => gt.GigId);
            modelBuilder.Entity<GigType>()
                .HasOne(gt => gt.Type)
                .WithMany()
                .HasForeignKey(gt => gt.TypeId);
            modelBuilder.Entity<Type>()
                .HasIndex(t => t.Name)
                .IsUnique(true);
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.BuyerId);
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Gig)
                .WithMany()
                .HasForeignKey(t => t.GigId);
        }
    }

}