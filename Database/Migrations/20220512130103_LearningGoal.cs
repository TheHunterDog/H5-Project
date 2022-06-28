using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class Leerdoelen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LearningGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Beschrijving = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leerdoelen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leerdoelen_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id");
                });
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentBegeleiders_StudentBegeleiderId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "LearningGoals");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentBegeleiderId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StudentBegeleiderId",
                table: "Student");
        }
    }
}
