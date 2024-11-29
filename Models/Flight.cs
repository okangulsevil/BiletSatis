using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiletSatis.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FlightNumber { get; set; } = string.Empty;

        [Required]
        public string Airline { get; set; } = string.Empty;

        [Required]
        public string Origin { get; set; } = string.Empty;

        [Required]
        public string Destination { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DepartureTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public TimeSpan FlightDuration => ArrivalTime - DepartureTime; // Uçuş Süresi

        [Required]
        public int Capacity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string? AirlineLogoPath { get; set; } // Firma logosunun dosya yolu

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}