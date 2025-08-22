using System.ComponentModel.DataAnnotations;

namespace ProjectBalanceLibrary.Models
{
    public class Lender
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        public List<Loan> Loans { get; set; } = new();
    }
}
