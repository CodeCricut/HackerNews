using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HackerNews.EF.Migrations
{
	public partial class dates : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<DateTime>(
				name: "JoinDate",
				table: "Users",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.AddColumn<DateTime>(
				name: "PostDate",
				table: "Comments",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.AddColumn<DateTime>(
				name: "PostDate",
				table: "Articles",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "JoinDate",
				table: "Users");

			migrationBuilder.DropColumn(
				name: "PostDate",
				table: "Comments");

			migrationBuilder.DropColumn(
				name: "PostDate",
				table: "Articles");
		}
	}
}
