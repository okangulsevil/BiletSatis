using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiletSatis.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; } // Rezervasyon Tarihi

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } // Navigation property

        [Required]
        [ForeignKey("Flight")]
        public int FlightId { get; set; }
        public Flight Flight { get; set; } // Navigation property

        [Required]
        public string SeatNumber { get; set; } // Koltuk Numarası

        [Required]
        [Range(1, 10000)]
        public decimal TotalPrice { get; set; } // Toplam Fiyat
    }
}