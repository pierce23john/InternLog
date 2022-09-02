using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternLog.Data.Migrations
{
	public partial class AddTimesheetUser : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<Guid>(
				name: "UserId",
				table: "Timesheets",
				type: "uniqueidentifier",
				nullable: false,
				defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

			migrationBuilder.CreateIndex(
				name: "IX_Timesheets_UserId",
				table: "Timesheets",
				column: "UserId");

			migrationBuilder.AddForeignKey(
				name: "FK_Timesheets_AspNetUsers_UserId",
				table: "Timesheets",
				column: "UserId",
				principalTable: "AspNetUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Timesheets_AspNetUsers_UserId",
				table: "Timesheets");

			migrationBuilder.DropIndex(
				name: "IX_Timesheets_UserId",
				table: "Timesheets");

			migrationBuilder.DropColumn(
				name: "UserId",
				table: "Timesheets");
		}
	}
}