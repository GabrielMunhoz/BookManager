using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManager.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPropNameOfLoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoanDate",
                table: "Loans",
                newName: "ReturnDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "Loans",
                newName: "LoanDate");
        }
    }
}
