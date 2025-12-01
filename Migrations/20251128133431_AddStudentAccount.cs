using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RijschoolHarmonieApp.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentAccounts",
                columns: table => new
                {
                    StudentAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TotalDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "[TotalCredit] - [TotalDebit]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAccounts", x => x.StudentAccountId);
                    table.ForeignKey(
                        name: "FK_StudentAccounts_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentAccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_StudentAccounts_StudentAccountId",
                        column: x => x.StudentAccountId,
                        principalTable: "StudentAccounts",
                        principalColumn: "StudentAccountId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_StudentAccountId",
                table: "Payment",
                column: "StudentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAccounts_StudentId",
                table: "StudentAccounts",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "StudentAccounts");
        }
    }
}
