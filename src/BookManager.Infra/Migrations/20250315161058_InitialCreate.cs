using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManager.Infra.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "UserBooks",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserBooks", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Loans",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LoanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UserBookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Loans", x => x.Id);
                table.ForeignKey(
                    name: "FK_Loans_UserBooks_UserBookId",
                    column: x => x.UserBookId,
                    principalTable: "UserBooks",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ReleaseYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                LoanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Books", x => x.Id);
                table.ForeignKey(
                    name: "FK_Books_Loans_LoanId",
                    column: x => x.LoanId,
                    principalTable: "Loans",
                    principalColumn: "Id");
            });

        migrationBuilder.InsertData(
            table: "UserBooks",
            columns: new[] { "Id", "CreateDate", "Email", "Name", "UpdateDate" },
            values: new object[] { new Guid("d0f606a2-622c-46b8-a844-ae0e817b1839"), new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "gabrielmunhoz@bookmanager.com", "Gabriel Munhoz", null });

        migrationBuilder.CreateIndex(
            name: "IX_Books_LoanId",
            table: "Books",
            column: "LoanId");

        migrationBuilder.CreateIndex(
            name: "IX_Loans_UserBookId",
            table: "Loans",
            column: "UserBookId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Books");

        migrationBuilder.DropTable(
            name: "Loans");

        migrationBuilder.DropTable(
            name: "UserBooks");
    }
}
