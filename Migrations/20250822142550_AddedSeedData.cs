using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectBalanceLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CopiesAvailable", "ISBN", "PublishedYear", "Title", "TotalCopies" },
                values: new object[,]
                {
                    { 1, "John Steinbeck", 5, "9780140177398", 1937, "Of Mice and Men", 5 },
                    { 2, "J.K. Rowling", 2, "9780439139595", 2000, "HP and the Goblet of Fire", 3 },
                    { 3, "J.R.R. Tolkien", 1, "9780261103573", 1954, "The Lord of the Rings", 4 }
                });

            migrationBuilder.InsertData(
                table: "Lenders",
                columns: new[] { "Id", "Email", "FullName" },
                values: new object[,]
                {
                    { 1, "alice@projectbalance.com", "Alice Johnson" },
                    { 2, "bob@projectbalance.com", "Bob Smith" },
                    { 3, "cindy@projectbalance.com", "Cindy Lee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lenders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lenders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lenders",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
