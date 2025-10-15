using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrayTimeBot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReminderHourToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReminderHour",
                table: "Users",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReminderHour",
                table: "Users");
        }
    }
}
