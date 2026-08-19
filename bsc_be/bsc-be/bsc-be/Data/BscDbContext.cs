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
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<GigType> GigTypes { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
            .Property(t => t.Status)
            .HasConversion<string>();
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique(true);
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique(true);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Password)
                .IsUnique(true);
            modelBuilder.Entity<User>()
                .Property(u => u.UserRole)
                .HasConversion<string>();
            modelBuilder.Entity<Gig>()
                .HasOne(g => g.User)
                .WithMany(u => u.Gigs)
                .HasForeignKey(g => g.UserId);
            modelBuilder.Entity<GigType>()
                .HasOne(gt => gt.Gig)
                .WithMany(g=>g.GigTypes)
                .HasForeignKey(gt => gt.GigId);
            modelBuilder.Entity<GigType>()
                .HasOne(gt => gt.Type)
                .WithMany(t=>t.GigTypes)
                .HasForeignKey(gt => gt.TypeId);
            modelBuilder.Entity<Type>()
                .HasIndex(t => t.Name)
                .IsUnique(true);
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u=>u.Transactions)
                .HasForeignKey(t => t.BuyerId);
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Gig)
                .WithMany(g=>g.Transactions)
                .HasForeignKey(t => t.GigId);
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