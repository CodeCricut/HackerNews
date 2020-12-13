using Microsoft.EntityFrameworkCore.Migrations;

namespace HackerNews.Infrastructure.Migrations
{
	public partial class imageNavigation : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			//migrationBuilder.DropForeignKey(
			//    name: "FK_Articles_Images_AssociatedImageId1",
			//    table: "Articles");

			//migrationBuilder.DropForeignKey(
			//    name: "FK_Boards_Images_BoardImageId1",
			//    table: "Boards");

			//migrationBuilder.DropForeignKey(
			//    name: "FK_Users_Images_ProfileImageId1",
			//    table: "Users");

			//migrationBuilder.DropIndex(
			//    name: "IX_Users_ProfileImageId1",
			//    table: "Users");

			//migrationBuilder.DropIndex(
			//    name: "IX_Boards_BoardImageId1",
			//    table: "Boards");

			//migrationBuilder.DropIndex(
			//    name: "IX_Articles_AssociatedImageId1",
			//    table: "Articles");

			//migrationBuilder.DropColumn(
			//    name: "ProfileImageId1",
			//    table: "Users");

			//migrationBuilder.DropColumn(
			//    name: "BoardImageId1",
			//    table: "Boards");

			//migrationBuilder.DropColumn(
			//    name: "AssociatedImageId1",
			//    table: "Articles");

			migrationBuilder.CreateIndex(
				name: "IX_Images_ArticleId",
				table: "Images",
				column: "ArticleId",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Images_BoardId",
				table: "Images",
				column: "BoardId",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Images_UserId",
				table: "Images",
				column: "UserId",
				unique: true);

			migrationBuilder.AddForeignKey(
				name: "FK_Images_Articles_ArticleId",
				table: "Images",
				column: "ArticleId",
				principalTable: "Articles",
				principalColumn: "Id",
				onDelete: ReferentialAction.NoAction);

			migrationBuilder.AddForeignKey(
				name: "FK_Images_Boards_BoardId",
				table: "Images",
				column: "BoardId",
				principalTable: "Boards",
				principalColumn: "Id",
				onDelete: ReferentialAction.NoAction);

			migrationBuilder.AddForeignKey(
				name: "FK_Images_Users_UserId",
				table: "Images",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "Id",
				onDelete: ReferentialAction.NoAction);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Images_Articles_ArticleId",
				table: "Images");

			migrationBuilder.DropForeignKey(
				name: "FK_Images_Boards_BoardId",
				table: "Images");

			migrationBuilder.DropForeignKey(
				name: "FK_Images_Users_UserId",
				table: "Images");

			migrationBuilder.DropIndex(
				name: "IX_Images_ArticleId",
				table: "Images");

			migrationBuilder.DropIndex(
				name: "IX_Images_BoardId",
				table: "Images");

			migrationBuilder.DropIndex(
				name: "IX_Images_UserId",
				table: "Images");

			migrationBuilder.AddColumn<int>(
				name: "ProfileImageId1",
				table: "Users",
				type: "int",
				nullable: true);

			migrationBuilder.AddColumn<int>(
				name: "BoardImageId1",
				table: "Boards",
				type: "int",
				nullable: true);

			migrationBuilder.AddColumn<int>(
				name: "AssociatedImageId1",
				table: "Articles",
				type: "int",
				nullable: true);

			migrationBuilder.CreateIndex(
				name: "IX_Users_ProfileImageId1",
				table: "Users",
				column: "ProfileImageId1");

			migrationBuilder.CreateIndex(
				name: "IX_Boards_BoardImageId1",
				table: "Boards",
				column: "BoardImageId1");

			migrationBuilder.CreateIndex(
				name: "IX_Articles_AssociatedImageId1",
				table: "Articles",
				column: "AssociatedImageId1");

			migrationBuilder.AddForeignKey(
				name: "FK_Articles_Images_AssociatedImageId1",
				table: "Articles",
				column: "AssociatedImageId1",
				principalTable: "Images",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Boards_Images_BoardImageId1",
				table: "Boards",
				column: "BoardImageId1",
				principalTable: "Images",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Users_Images_ProfileImageId1",
				table: "Users",
				column: "ProfileImageId1",
				principalTable: "Images",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}
	}
}
