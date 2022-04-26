using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPF.Migrations
{
    public partial class AddDateToSBG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentBegeleiderGesprekken",
                table: "StudentBegeleiderGesprekken");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentBegeleiderGesprekken",
                table: "StudentBegeleiderGesprekken",
                columns: new[] { "StudentId", "StudentBegeleiderId", "GesprekDatum" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentBegeleiderGesprekken",
                table: "StudentBegeleiderGesprekken");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentBegeleiderGesprekken",
                table: "StudentBegeleiderGesprekken",
                columns: new[] { "StudentId", "StudentBegeleiderId" });
        }
    }
}
