using Microsoft.EntityFrameworkCore;
using ProjectBalanceLibrary.Contracts;
using ProjectBalanceLibrary.Data;
using ProjectBalanceLibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectBalanceLibrary.Services
{
    public class LenderService : ILenderService
    {
        private readonly AppDbContext _db;
        public LenderService(AppDbContext db) => _db = db;


        public Task<List<Lender>> GetAllAsync() => _db.Lenders.AsNoTracking().OrderBy(l => l.FullName).ToListAsync();
        public Task<Lender?> GetAsync(int id) => _db.Lenders.FindAsync(id).AsTask();


        public async Task<Lender> CreateAsync(Lender input)
        {
            Validate(input);
            _db.Lenders.Add(input);
            await _db.SaveChangesAsync();
            return input;
        }


        public async Task<Lender> UpdateAsync(Lender input)
        {
            Validate(input);
            var existing = await _db.Lenders.FirstOrDefaultAsync(l => l.Id == input.Id) ?? throw new InvalidOperationException("Lender not found");
            existing.FullName = input.FullName.Trim();
            existing.Email = input.Email.Trim();
            await _db.SaveChangesAsync();
            return existing;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Lenders.FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException("Lender not found");
            _db.Lenders.Remove(entity);
            await _db.SaveChangesAsync();
        }

        private static void Validate(Lender input)
        {
            if (string.IsNullOrWhiteSpace(input.FullName))
                throw new InvalidOperationException("Full name is required.");

            var email = input.Email?.Trim();
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidOperationException("Email is required.");

            var emailAttr = new EmailAddressAttribute();
            if (!emailAttr.IsValid(email))
                throw new InvalidOperationException("Email format is invalid.");
        }
    }
}
