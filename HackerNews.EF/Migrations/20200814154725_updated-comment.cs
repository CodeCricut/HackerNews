using Microsoft.EntityFrameworkCore.Migrations;

namespace HackerNews.EF.Migrations
{
    public partial class updatedcomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "ParentArticleId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentArticleId",
                table: "Comments",
                column: "ParentArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Articles_ParentArticleId",
                table: "Comments",
                column: "ParentArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Articles_ParentArticleId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ParentArticleId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ParentArticleId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
