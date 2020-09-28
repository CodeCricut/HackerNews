using Microsoft.EntityFrameworkCore.Migrations;

namespace HackerNews.EF.Migrations
{
    public partial class usersaves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Articles_Users_AuthorId",
            //    table: "Articles");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Articles_Users_UserId",
            //    table: "Articles");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Comments_Users_AuthorId",
            //    table: "Comments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Comments_Users_UserId",
            //    table: "Comments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Comments_AuthorId",
            //    table: "Comments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Articles_AuthorId",
            //    table: "Articles");

            //migrationBuilder.DropColumn(
            //    name: "AuthorId",
            //    table: "Comments");

            //migrationBuilder.DropColumn(
            //    name: "AuthorId",
            //    table: "Articles");

            //migrationBuilder.AlterColumn<int>(
            //    name: "UserId",
            //    table: "Comments",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "UserId",
            //    table: "Articles",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserArticle",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ArticleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArticle", x => new { x.UserId, x.ArticleId });
                    table.ForeignKey(
                        name: "FK_UserArticle_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserArticle_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserComment",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    CommentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComment", x => new { x.UserId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_UserComment_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserComment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserArticle_ArticleId",
                table: "UserArticle",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserComment_CommentId",
                table: "UserComment",
                column: "CommentId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Articles_Users_UserId",
            //    table: "Articles",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Comments_Users_UserId",
            //    table: "Comments",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_UserId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "UserArticle");

            migrationBuilder.DropTable(
                name: "UserComment");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Articles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_UserId",
                table: "Articles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
