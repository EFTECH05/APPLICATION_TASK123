using Microsoft.EntityFrameworkCore;

namespace PaymentSystem.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<UserUpdateRequest> UserUpdateRequests { get; set; }
    }
}