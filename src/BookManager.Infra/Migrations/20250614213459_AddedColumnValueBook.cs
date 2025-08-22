using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManager.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnValueBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Books");
        }
    }
}
