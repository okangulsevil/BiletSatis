using System.ComponentModel.DataAnnotations;

namespace BiletSatis.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public ICollection<Ticket> Tickets { get; set; } // Navigation property
    }
}