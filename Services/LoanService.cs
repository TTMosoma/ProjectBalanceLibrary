using Microsoft.EntityFrameworkCore;
using ProjectBalanceLibrary.Contracts;
using ProjectBalanceLibrary.Data;
using ProjectBalanceLibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectBalanceLibrary.Services
{
    public class LoanService : ILoanService
    {
        private readonly AppDbContext _db;
        public LoanService(AppDbContext db) => _db = db;


        public Task<List<Loan>> GetAllAsync() => _db.Loans
        .Include(l => l.Book)
        .Include(l => l.Lender)
        .AsNoTracking()
        .OrderByDescending(l => l.LoanDateUtc)
        .ToListAsync();


        public Task<List<Loan>> GetActiveAsync() => _db.Loans
        .Include(l => l.Book)
        .Include(l => l.Lender)
        .Where(l => l.ReturnDateUtc == null)
        .AsNoTracking()
        .OrderBy(l => l.DueDateUtc)
        .ToListAsync();

        public async Task<Loan> CreateLoanAsync(int bookId, int lenderId, DateTime? loanDateUtc = null)
        {
            var utcNow = loanDateUtc ?? DateTime.UtcNow;
            using var tx = await _db.Database.BeginTransactionAsync();
            
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == bookId) ?? throw new InvalidOperationException("Book not found");
            var lender = await _db.Lenders.FirstOrDefaultAsync(l => l.Id == lenderId) ?? throw new InvalidOperationException("Lender not found");


            if (book.CopiesAvailable <= 0)
                throw new InvalidOperationException("No copies available for this book.");


            // Rules: block if lender has overdue loans
            var hasOverdue = await _db.Loans.AnyAsync(l => l.LenderId == lenderId && l.ReturnDateUtc == null && l.DueDateUtc < utcNow);
            if (hasOverdue)
                throw new InvalidOperationException("Lender has overdue loans and cannot borrow new books.");


            // Rule: lender cannot have >5 active loans
            var activeCount = await _db.Loans.CountAsync(l => l.LenderId == lenderId && l.ReturnDateUtc == null);
            if (activeCount >= 5)
                throw new InvalidOperationException("Lender already has 5 active loans.");


            // Rule: prevent duplicate active loan for same book/lender
            var duplicateActive = await _db.Loans.AnyAsync(l => l.BookId == bookId && l.LenderId == lenderId && l.ReturnDateUtc == null);
            if (duplicateActive)
                throw new InvalidOperationException("This lender already has an active loan for this book.");


            // Create loan
            var loan = new Loan
            {
                BookId = bookId,
                LenderId = lenderId,
                LoanDateUtc = utcNow,
                DueDateUtc = utcNow.AddDays(14),
                ReturnDateUtc = null
            };


            // Decrement copies
            book.CopiesAvailable -= 1;


            _db.Loans.Add(loan);
            await _db.SaveChangesAsync();
            await tx.CommitAsync();
            return loan;
        }
        public async Task<Loan> ReturnLoanAsync(int loanId, DateTime? returnDateUtc = null)
        {
            var utcNow = returnDateUtc ?? DateTime.UtcNow;
            using var tx = await _db.Database.BeginTransactionAsync();


            var loan = await _db.Loans.Include(l => l.Book).FirstOrDefaultAsync(l => l.Id == loanId) ?? throw new InvalidOperationException("Loan not found");
            if (loan.ReturnDateUtc != null)
                throw new InvalidOperationException("Loan already returned.");


            loan.ReturnDateUtc = utcNow;
            // Increment copies
            if (loan.Book != null)
            {
                loan.Book.CopiesAvailable += 1;
            }


            await _db.SaveChangesAsync();
            await tx.CommitAsync();
            return loan;
        }
    }
}
