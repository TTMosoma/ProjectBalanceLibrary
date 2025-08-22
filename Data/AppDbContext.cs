using Microsoft.EntityFrameworkCore;
using ProjectBalanceLibrary.Models;

namespace ProjectBalanceLibrary.Data
{
  public class AppDbContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Lender> Lenders => Set<Lender>();
        public DbSet<Loan> Loans => Set<Loan>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.ISBN).IsRequired();
                b.HasIndex(x => x.ISBN).IsUnique(); // THIS IS CRITICAL, ISBN MUST BE UNIQUE
                b.Property(x => x.Title).IsRequired();
                b.Property(x => x.Author).IsRequired();
                b.ToTable(t => t.HasCheckConstraint("CK_Book_Copies",
                "CopiesAvailable <= TotalCopies AND CopiesAvailable >= 0 AND TotalCopies >= 0"));
            });

            modelBuilder.Entity<Lender>(l =>
            {
                l.HasKey(x => x.Id);
                l.Property(x => x.FullName).IsRequired();
                l.Property(x => x.Email).IsRequired();
            });

            modelBuilder.Entity<Loan>(l =>
            {
                l.HasKey(x => x.Id);
                l.Property(x => x.LoanDateUtc).IsRequired();
                l.Property(x => x.DueDateUtc).IsRequired();
                l.HasOne(x => x.Book)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Cascade);
                l.HasOne(x => x.Lender)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.LenderId)
                .OnDelete(DeleteBehavior.Cascade);
            });


            //  data seeding for the exercise as requested 
            modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, ISBN = "9780140177398", Title = "Of Mice and Men", Author = "John Steinbeck", PublishedYear = 1937, TotalCopies = 5, CopiesAvailable = 5 },
            new Book { Id = 2, ISBN = "9780439139595", Title = "HP and the Goblet of Fire", Author = "J.K. Rowling", PublishedYear = 2000, TotalCopies = 3, CopiesAvailable = 2 },
            new Book { Id = 3, ISBN = "9780261103573", Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", PublishedYear = 1954, TotalCopies = 4, CopiesAvailable = 1 }
            );


            modelBuilder.Entity<Lender>().HasData(
            new Lender { Id = 1, FullName = "Alice Johnson", Email = "alice@projectbalance.com" },
            new Lender { Id = 2, FullName = "Bob Smith", Email = "bob@projectbalance.com" },
            new Lender { Id = 3, FullName = "Cindy Lee", Email = "cindy@projectbalance.com" }
            );
        }
    }
}
