using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RijschoolHarmonieApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "InstructorPrices",
                columns: table => new
                {
                    InstructorPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    LessonPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExamPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorPrices", x => x.InstructorPriceId);
                    table.ForeignKey(
                        name: "FK_InstructorPrices_Users_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAccounts",
                columns: table => new
                {
                    StudentAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TotalDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "[TotalDebit] - [TotalCredit]")
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
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentAccountId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_StudentAccounts_StudentAccountId",
                        column: x => x.StudentAccountId,
                        principalTable: "StudentAccounts",
                        principalColumn: "StudentAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstructorPrices_InstructorId",
                table: "InstructorPrices",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StudentAccountId",
                table: "Payments",
                column: "StudentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAccounts_StudentId",
                table: "StudentAccounts",
                column: "StudentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstructorPrices");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "StudentAccounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
