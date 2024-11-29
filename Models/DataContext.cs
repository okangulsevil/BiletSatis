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
            base.OnModelCreating(modelBuilder);

            // Flight - Ticket ilişkilendirmesi
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Flight)
                .WithMany(f => f.Tickets)
                .HasForeignKey(t => t.FlightId)
                .OnDelete(DeleteBehavior.Cascade); // Uçuş silindiğinde ilgili biletler de silinir

            // Ticket - Customer ilişkilendirmesi
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.SetNull); // Müşteri silindiğinde biletler boşa çıkar

            // Varsayılan değerler ve diğer ayarlar
            modelBuilder.Entity<Ticket>()
                .Property(t => t.IsBooked)
                .HasDefaultValue(false); // Varsayılan olarak biletler rezerve edilmemiş olur
        }
    }
}