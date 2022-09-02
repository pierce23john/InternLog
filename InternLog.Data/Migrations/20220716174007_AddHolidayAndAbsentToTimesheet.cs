using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternLog.Data.Migrations
{
    public partial class AddHolidayAndAbsentToTimesheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAbsent",
                table: "Timesheets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHoliday",
                table: "Timesheets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAbsent",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "IsHoliday",
                table: "Timesheets");
        }
    }
}
