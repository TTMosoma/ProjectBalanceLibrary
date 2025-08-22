namespace ProjectBalanceLibrary.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int LenderId { get; set; }

        public DateTime LoanDateUtc { get; set; }
        public DateTime DueDateUtc { get; set; }
        public DateTime? ReturnDateUtc { get; set; }
        public Book? Book { get; set; }
        public Lender? Lender { get; set; }
        public bool IsReturned => ReturnDateUtc.HasValue;
        public bool IsOverdue(DateTime utcNow) => !IsReturned && DueDateUtc < utcNow;
    }
}
