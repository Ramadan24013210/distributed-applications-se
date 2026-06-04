using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReminderCalendar.Migrations
{
 
    public partial class FixCategory : Migration
    {
     
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NotificationType",
                table: "Reminders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Reminders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_EventId",
                table: "Reminders",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Events_EventId",
                table: "Reminders",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Events_EventId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_EventId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserId",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "NotificationType",
                table: "Reminders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Reminders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
