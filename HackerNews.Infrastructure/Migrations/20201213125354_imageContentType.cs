using Microsoft.EntityFrameworkCore.Migrations;

namespace HackerNews.Infrastructure.Migrations
{
	public partial class imageContentType : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "ProfileImageId",
				table: "Users");

			migrationBuilder.DropColumn(
				name: "BoardImageId",
				table: "Boards");

			migrationBuilder.DropColumn(
				name: "AssociatedImageId",
				table: "Articles");

			migrationBuilder.AddColumn<string>(
				name: "ContentType",
				table: "Images",
				nullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "ContentType",
				table: "Images");

			migrationBuilder.AddColumn<int>(
				name: "ProfileImageId",
				table: "Users",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<int>(
				name: "BoardImageId",
				table: "Boards",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<int>(
				name: "AssociatedImageId",
				table: "Articles",
				type: "int",
				nullable: false,
				defaultValue: 0);
		}
	}
}
