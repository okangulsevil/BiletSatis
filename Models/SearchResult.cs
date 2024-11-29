namespace BiletSatis.Models
{
    public class SearchResult
    {
        public int Id { get; set; }       // Uçuş Id'si
        public string Title { get; set; } // Uçuş Numarası
        public string Description { get; set; } // Uçuş Detayları
        public string ExtraInfo { get; set; }   // Boş Koltuk Bilgisi
        public string Link { get; set; }        // Rezervasyon Bağlantısı
    }
}