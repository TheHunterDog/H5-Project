using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class subjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudentBegeleiders",
                keyColumn: "Id",
                keyValue: 964);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1147576);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1147577);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1152882);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1156618);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1159362);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1160918);

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentProblems");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.InsertData(
                table: "StudentBegeleiders",
                columns: new[] { "Id", "Docentcode", "Naam" },
                values: new object[] { 964, "SA1234", "Karen brakband" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Achternaam", "Klasscode", "StudentbegeleiderId", "Studentnummer", "Tussenvoegsel", "Voornaam" },
                values: new object[,]
                {
                    { 1147576, "Jaap", "OOSDDH2023", 0, "s1147576", "Jappie", "Jan" },
                    { 1147577, "Jongedijk", "OOSDDH2022", 0, "s1147577", "", "Tristan" },
                    { 1152882, "Hutten", "OOSDDH2022", 0, "s1152882", "", "Rob" },
                    { 1156618, "Heijnekamp", "OOSDDH2022", 0, "s1156618", "", "Mark" },
                    { 1159362, "Pijlgroms", "OOSDDH2022", 0, "s1159362", "", "Antoine" },
                    { 1160918, "Nijsink", "OOSDDH2022", 0, "s1160918", "", "Evert-Jan" }
                });
        }
    }
}
