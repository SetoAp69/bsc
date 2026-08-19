using Microsoft.EntityFrameworkCore;

namespace bsc_be.Models
{
    public class BscDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Type> Types { get; set; }
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
        }
    }

}