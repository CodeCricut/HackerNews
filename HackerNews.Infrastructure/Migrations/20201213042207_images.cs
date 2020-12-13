using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackerNews.Infrastructure.Migrations
{
    public partial class images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileImageId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfileImageId1",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BoardImageId",
                table: "Boards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BoardImageId1",
                table: "Boards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssociatedImageId",
                table: "Articles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AssociatedImageId1",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    ImageTitle = table.Column<string>(nullable: true),
                    ImageDescription = table.Column<string>(nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true),
                    ArticleId = table.Column<int>(nullable: false),
                    BoardId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Images");

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
                name: "ProfileImageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileImageId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BoardImageId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "BoardImageId1",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "AssociatedImageId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AssociatedImageId1",
                table: "Articles");
        }
    }
}
