using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternLog.Data.Migrations
{
	public partial class AddTimesheet : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Timesheets",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Timesheets", x => x.Id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Timesheets");
		}
	}
}