using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class NotificationsRemoveSender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_StudentSupervisor_ReceiverId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_StudentSupervisor_SenderId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SenderId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Notifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_StudentSupervisor_ReceiverId",
                table: "Notifications",
                column: "ReceiverId",
                principalTable: "StudentSupervisor",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_StudentSupervisor_ReceiverId",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SenderId",
                table: "Notifications",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_StudentSupervisor_ReceiverId",
                table: "Notifications",
                column: "ReceiverId",
                principalTable: "StudentSupervisor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_StudentSupervisor_SenderId",
                table: "Notifications",
                column: "SenderId",
                principalTable: "StudentSupervisor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
