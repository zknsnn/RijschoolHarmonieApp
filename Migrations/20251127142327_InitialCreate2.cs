using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RijschoolHarmonieApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_InstructorPrices_InstructorId",
                table: "InstructorPrices",
                column: "InstructorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstructorPrices");
        }
    }
}
