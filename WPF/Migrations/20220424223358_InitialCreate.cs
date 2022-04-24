using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentBegeleiders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Docentcode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentBegeleiders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Studentnummer = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tussenvoegsel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Achternaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Klasscode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentbegeleiderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_StudentBegeleiders_StudentbegeleiderId",
                        column: x => x.StudentbegeleiderId,
                        principalTable: "StudentBegeleiders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentBegeleiderGesprekkens",
                columns: table => new
                {
                    StudentBegeleiderId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    GesprekDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Voltooid = table.Column<bool>(type: "bit", nullable: false),
                    Opmerkingen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentBegeleiderGesprekkens", x => new { x.StudentId, x.StudentBegeleiderId });
                    table.ForeignKey(
                        name: "FK_StudentBegeleiderGesprekkens_StudentBegeleiders_StudentBegeleiderId",
                        column: x => x.StudentBegeleiderId,
                        principalTable: "StudentBegeleiders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentBegeleiderGesprekkens_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentBegeleiderGesprekkens_StudentBegeleiderId",
                table: "StudentBegeleiderGesprekkens",
                column: "StudentBegeleiderId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBegeleiders_Docentcode",
                table: "StudentBegeleiders",
                column: "Docentcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentbegeleiderId",
                table: "Students",
                column: "StudentbegeleiderId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Studentnummer",
                table: "Students",
                column: "Studentnummer",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentBegeleiderGesprekkens");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "StudentBegeleiders");
        }
    }
}
