using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiletSatis.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FlightId { get; set; }

        [Required]
        public Flight Flight { get; set; } = null!; // Null atanamaz olarak işaretlendi

        public int? CustomerId { get; set; }

        public Customer? Customer { get; set; }

        [Required]
        public string SeatNumber { get; set; } = string.Empty; // Varsayılan değer eklendi

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        public bool IsBooked { get; set; } = false; // Varsayılan değer eklendi
    }
}