using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBalanceLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddedBusinessRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Books_ISBN",
                table: "Books",
                column: "ISBN",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Book_Copies",
                table: "Books",
                sql: "CopiesAvailable <= TotalCopies AND CopiesAvailable >= 0 AND TotalCopies >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_ISBN",
                table: "Books");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Book_Copies",
                table: "Books");
        }
    }
}
