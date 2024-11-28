using Microsoft.EntityFrameworkCore;

namespace BiletSatis.Models
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Flight> Flights => Set<Flight>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ticket - Customer ilişkisi
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CustomerId);

            // Ticket - Flight ilişkisi
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Flight)
                .WithMany(f => f.Tickets)
                .HasForeignKey(t => t.FlightId);
        }
    }
}