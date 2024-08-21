using ConsumerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsumerService.Infrastructure.Persistence
{
    public class ConsumerDbContext : DbContext
    {
        public ConsumerDbContext(DbContextOptions<ConsumerDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
               
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                // Add further configurations here...
            });
        }
    }
}
