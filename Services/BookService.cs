using Microsoft.EntityFrameworkCore;
using ProjectBalanceLibrary.Contracts;
using ProjectBalanceLibrary.Data;
using ProjectBalanceLibrary.Models;

namespace ProjectBalanceLibrary.Services
{
    internal class BookService : IBookService
    {
        private readonly AppDbContext _db;
        public BookService(AppDbContext db) => _db = db;
        public Task<List<Book>> GetAllAsync() => _db.Books.AsNoTracking().OrderBy(b => b.Title).ToListAsync();
        public Task<Book?> GetAsync(int id) => _db.Books.FindAsync(id).AsTask();

        public async Task<Book> CreateAsync(Book input)
        {
            await ValidateAsync(input, isUpdate: false);
            _db.Books.Add(input);
            await _db.SaveChangesAsync();
            return input;
        }
        public async Task<Book> UpdateAsync(Book input)
        {
            await ValidateAsync(input, isUpdate: true);
            var existing = await _db.Books.FirstOrDefaultAsync(b => b.Id == input.Id) ?? throw new InvalidOperationException("Book not found");
            existing.ISBN = input.ISBN.Trim();
            existing.Title = input.Title.Trim();
            existing.Author = input.Author.Trim();
            existing.PublishedYear = input.PublishedYear;
            existing.TotalCopies = input.TotalCopies;
            existing.CopiesAvailable = input.CopiesAvailable;
            await _db.SaveChangesAsync();
            return existing;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Books.FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException("Book not found");
            _db.Books.Remove(entity);
            await _db.SaveChangesAsync();
        }

        private async Task ValidateAsync(Book input, bool isUpdate)
        {
            input.ISBN = input.ISBN?.Trim() ?? string.Empty;
            input.Title = input.Title?.Trim() ?? string.Empty;
            input.Author = input.Author?.Trim() ?? string.Empty;

            if (input.CopiesAvailable > input.TotalCopies)
                throw new InvalidOperationException("CopiesAvailable cannot exceed TotalCopies.");

            var exists = await _db.Books
            .AnyAsync(b => b.ISBN == input.ISBN && (!isUpdate || b.Id != input.Id));
            if (exists)
                throw new InvalidOperationException("ISBN must be unique.");
        }
    }
}
