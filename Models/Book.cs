using System.ComponentModel.DataAnnotations;

namespace ProjectBalanceLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(32)]
        public string ISBN { get; set; } = string.Empty; // Unique


        [Required]
        [MaxLength(256)]
        public string Title { get; set; } = string.Empty;


        [Required]
        [MaxLength(128)]
        public string Author { get; set; } = string.Empty;


        public int PublishedYear { get; set; }


        [Range(0, int.MaxValue)]
        public int TotalCopies { get; set; }


        [Range(0, int.MaxValue)]
        public int CopiesAvailable { get; set; }


        public List<Loan> Loans { get; set; } = new();
    }
}
