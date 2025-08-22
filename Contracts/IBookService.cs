using ProjectBalanceLibrary.Models;

namespace ProjectBalanceLibrary.Contracts
{
    public interface IBookService
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetAsync(int id);
        Task<Book> CreateAsync(Book input);
        Task<Book> UpdateAsync(Book input);
        Task DeleteAsync(int id);
    }
}
