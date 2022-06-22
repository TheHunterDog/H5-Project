using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class Authentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "StudentSupervisors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "StudentSupervisors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "StudentSupervisors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
            
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Teachers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentBegeleiders_StudentBegeleiderId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "LearningGoals");

            migrationBuilder.DropTable(
                name: "StudentProblems");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentBegeleiderId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentBegeleiderId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "StudentSupervisors");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "StudentSupervisors");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "StudentSupervisors");

            migrationBuilder.InsertData(
                table: "StudentSupervisors",
                columns: new[] { "Id", "TeacherCode", "Name" },
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
