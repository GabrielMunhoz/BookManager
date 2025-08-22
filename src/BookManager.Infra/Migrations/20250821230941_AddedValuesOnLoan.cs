using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManager.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddedValuesOnLoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PayedValue",
                table: "Loans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "Loans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayedValue",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "Loans");
        }
    }
}
