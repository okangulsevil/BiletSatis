using System.ComponentModel.DataAnnotations;

namespace BiletSatis.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public string? FlightNumber { get; set; } // Uçuş Numarası
        public string? Origin { get; set; }      // Kalkış Yeri
        public string? Destination { get; set; } // Varış Yeri

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DepartureTime { get; set; } // Kalkış Zamanı

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ArrivalTime { get; set; }   // Varış Zamanı
        public int Capacity { get; set; }       // Koltuk Kapasitesi
        public decimal Price { get; set; }      // Bilet Ücreti
        public ICollection<Ticket> Tickets { get; set; } // Navigation property
    }
}