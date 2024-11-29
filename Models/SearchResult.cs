namespace BiletSatis.Models
{
    public class SearchResult
    {
        public int Id { get; set; }          // Uçuş Id'si
        public string Title { get; set; }   // Uçuş Numarası
        public string Origin { get; set; }  // Kalkış 
        public string Destination { get; set; }  // Varış
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TimeSpan FlightDuration => ArrivalTime - DepartureTime; // Uçuş Süresi
        public string ExtraInfo { get; set; }   // Boş Koltuk Bilgisi
        public string Link { get; set; }        // Rezervasyon Bağlantısı
        public string Airline { get; set; }     // Firma Adı
        public string? AirlineLogoPath { get; set; }   // Firma Logosu Dosya Yolu
    }
}