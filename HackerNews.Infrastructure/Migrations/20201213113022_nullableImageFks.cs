using Microsoft.EntityFrameworkCore.Migrations;

namespace HackerNews.Infrastructure.Migrations
{
	public partial class nullableImageFks : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
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

			migrationBuilder.AlterColumn<int>(
				name: "UserId",
				table: "Images",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AlterColumn<int>(
				name: "BoardId",
				table: "Images",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AlterColumn<int>(
				name: "ArticleId",
				table: "Images",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.CreateIndex(
				name: "IX_Images_ArticleId",
				table: "Images",
				column: "ArticleId",
				unique: true,
				filter: "[ArticleId] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_Images_BoardId",
				table: "Images",
				column: "BoardId",
				unique: true,
				filter: "[BoardId] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_Images_UserId",
				table: "Images",
				column: "UserId",
				unique: true,
				filter: "[UserId] IS NOT NULL");

			migrationBuilder.AddForeignKey(
				name: "FK_Images_Articles_ArticleId",
				table: "Images",
				column: "ArticleId",
				principalTable: "Articles",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Images_Boards_BoardId",
				table: "Images",
				column: "BoardId",
				principalTable: "Boards",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Images_Users_UserId",
				table: "Images",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
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

			migrationBuilder.AlterColumn<int>(
				name: "UserId",
				table: "Images",
				type: "int",
				nullable: false,
				oldClrType: typeof(int),
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "BoardId",
				table: "Images",
				type: "int",
				nullable: false,
				oldClrType: typeof(int),
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "ArticleId",
				table: "Images",
				type: "int",
				nullable: false,
				oldClrType: typeof(int),
				oldNullable: true);

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
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Images_Boards_BoardId",
				table: "Images",
				column: "BoardId",
				principalTable: "Boards",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Images_Users_UserId",
				table: "Images",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
