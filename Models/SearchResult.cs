namespace BiletSatis.Models
{
    public class SearchResult
    {
        public string Type { get; set; }       // Model türü (Customer, Flight, Ticket)
        public string Title { get; set; }      // Sonuç başlığı
        public string Description { get; set; } // Ek bilgi
        public string Link { get; set; }       // Detay sayfasına bağlantı
    }
}