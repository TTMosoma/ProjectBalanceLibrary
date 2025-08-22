using ProjectBalanceLibrary.Models;

namespace ProjectBalanceLibrary.Contracts
{
    public interface ILoanService
    {
        Task<List<Loan>> GetAllAsync();
        Task<List<Loan>> GetActiveAsync();
        Task<Loan> CreateLoanAsync(int bookId, int lenderId, DateTime? loanDateUtc = null);
        Task<Loan> ReturnLoanAsync(int loanId, DateTime? returnDateUtc = null);
    }
}
