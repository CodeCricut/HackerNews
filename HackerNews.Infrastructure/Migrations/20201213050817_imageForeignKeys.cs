using Microsoft.EntityFrameworkCore.Migrations;

namespace HackerNews.Infrastructure.Migrations
{
	public partial class imageForeignKeys : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Articles_Images_AssociatedImageId1",
				table: "Articles");

			migrationBuilder.DropForeignKey(
				name: "FK_Boards_Images_BoardImageId1",
				table: "Boards");

			migrationBuilder.DropForeignKey(
				name: "FK_Users_Images_ProfileImageId1",
				table: "Users");

			migrationBuilder.DropIndex(
				name: "IX_Users_ProfileImageId1",
				table: "Users");

			migrationBuilder.DropIndex(
				name: "IX_Boards_BoardImageId1",
				table: "Boards");

			migrationBuilder.DropIndex(
				name: "IX_Articles_AssociatedImageId1",
				table: "Articles");

			migrationBuilder.DropColumn(
			   name: "ProfileImageId1",
			   table: "Users");

			migrationBuilder.DropColumn(
				name: "BoardImageId1",
				table: "Boards");

			migrationBuilder.DropColumn(
				name: "AssociatedImageId1",
				table: "Articles");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{

		}
	}
}
