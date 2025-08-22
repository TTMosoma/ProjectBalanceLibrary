using ProjectBalanceLibrary.Models;

namespace ProjectBalanceLibrary.Contracts
{
    public interface ILenderService
    {
        Task<List<Lender>> GetAllAsync();
        Task<Lender?> GetAsync(int id);
        Task<Lender> CreateAsync(Lender input);
        Task<Lender> UpdateAsync(Lender input);
        Task DeleteAsync(int id);
    }
}
