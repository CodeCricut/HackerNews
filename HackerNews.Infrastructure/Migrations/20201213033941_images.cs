using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackerNews.EF.Migrations
{
    public partial class images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_BoardUserModerator_Boards_BoardId",
            //    table: "BoardUserModerator");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_BoardUserModerator_Users_UserId",
            //    table: "BoardUserModerator");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_BoardUserSubscriber_Boards_BoardId",
            //    table: "BoardUserSubscriber");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_BoardUserSubscriber_Users_UserId",
            //    table: "BoardUserSubscriber");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserArticle_Articles_ArticleId",
            //    table: "UserArticle");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserArticle_Users_UserId",
            //    table: "UserArticle");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserArticleDislikes_Articles_ArticleId",
            //    table: "UserArticleDislikes");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserArticleDislikes_Users_UserId",
            //    table: "UserArticleDislikes");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserArticleLikes_Articles_ArticleId",
            //    table: "UserArticleLikes");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserArticleLikes_Users_UserId",
            //    table: "UserArticleLikes");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserComment_Comments_CommentId",
            //    table: "UserComment");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserComment_Users_UserId",
            //    table: "UserComment");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserCommentDislikes_Comments_CommentId",
            //    table: "UserCommentDislikes");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserCommentDislikes_Users_UserId",
            //    table: "UserCommentDislikes");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserCommentLikes_Comments_CommentId",
            //    table: "UserCommentLikes");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserCommentLikes_Users_UserId",
            //    table: "UserCommentLikes");

            migrationBuilder.AddColumn<int>(
                name: "ProfileImageId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BoardImageId",
                table: "Boards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssociatedImageId",
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
                    ImageData = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileImageId",
                table: "Users",
                column: "ProfileImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_BoardImageId",
                table: "Boards",
                column: "BoardImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AssociatedImageId",
                table: "Articles",
                column: "AssociatedImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Images_AssociatedImageId",
                table: "Articles",
                column: "AssociatedImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Images_BoardImageId",
                table: "Boards",
                column: "BoardImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_BoardUserModerator_Boards_BoardId",
            //    table: "BoardUserModerator",
            //    column: "BoardId",
            //    principalTable: "Boards",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_BoardUserModerator_Users_UserId",
            //    table: "BoardUserModerator",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_BoardUserSubscriber_Boards_BoardId",
            //    table: "BoardUserSubscriber",
            //    column: "BoardId",
            //    principalTable: "Boards",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_BoardUserSubscriber_Users_UserId",
            //    table: "BoardUserSubscriber",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserArticle_Articles_ArticleId",
            //    table: "UserArticle",
            //    column: "ArticleId",
            //    principalTable: "Articles",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserArticle_Users_UserId",
            //    table: "UserArticle",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserArticleDislikes_Articles_ArticleId",
            //    table: "UserArticleDislikes",
            //    column: "ArticleId",
            //    principalTable: "Articles",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserArticleDislikes_Users_UserId",
            //    table: "UserArticleDislikes",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserArticleLikes_Articles_ArticleId",
            //    table: "UserArticleLikes",
            //    column: "ArticleId",
            //    principalTable: "Articles",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserArticleLikes_Users_UserId",
            //    table: "UserArticleLikes",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserComment_Comments_CommentId",
            //    table: "UserComment",
            //    column: "CommentId",
            //    principalTable: "Comments",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserComment_Users_UserId",
            //    table: "UserComment",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserCommentDislikes_Comments_CommentId",
            //    table: "UserCommentDislikes",
            //    column: "CommentId",
            //    principalTable: "Comments",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserCommentDislikes_Users_UserId",
            //    table: "UserCommentDislikes",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserCommentLikes_Comments_CommentId",
            //    table: "UserCommentLikes",
            //    column: "CommentId",
            //    principalTable: "Comments",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserCommentLikes_Users_UserId",
            //    table: "UserCommentLikes",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Images_ProfileImageId",
                table: "Users",
                column: "ProfileImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Images_AssociatedImageId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Images_BoardImageId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardUserModerator_Boards_BoardId",
                table: "BoardUserModerator");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardUserModerator_Users_UserId",
                table: "BoardUserModerator");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardUserSubscriber_Boards_BoardId",
                table: "BoardUserSubscriber");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardUserSubscriber_Users_UserId",
                table: "BoardUserSubscriber");

            migrationBuilder.DropForeignKey(
                name: "FK_UserArticle_Articles_ArticleId",
                table: "UserArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_UserArticle_Users_UserId",
                table: "UserArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_UserArticleDislikes_Articles_ArticleId",
                table: "UserArticleDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserArticleDislikes_Users_UserId",
                table: "UserArticleDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserArticleLikes_Articles_ArticleId",
                table: "UserArticleLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserArticleLikes_Users_UserId",
                table: "UserArticleLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserComment_Comments_CommentId",
                table: "UserComment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserComment_Users_UserId",
                table: "UserComment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommentDislikes_Comments_CommentId",
                table: "UserCommentDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommentDislikes_Users_UserId",
                table: "UserCommentDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommentLikes_Comments_CommentId",
                table: "UserCommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommentLikes_Users_UserId",
                table: "UserCommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Images_ProfileImageId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfileImageId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Boards_BoardImageId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AssociatedImageId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BoardImageId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "AssociatedImageId",
                table: "Articles");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUserModerator_Boards_BoardId",
                table: "BoardUserModerator",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUserModerator_Users_UserId",
                table: "BoardUserModerator",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUserSubscriber_Boards_BoardId",
                table: "BoardUserSubscriber",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUserSubscriber_Users_UserId",
                table: "BoardUserSubscriber",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserArticle_Articles_ArticleId",
                table: "UserArticle",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserArticle_Users_UserId",
                table: "UserArticle",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserArticleDislikes_Articles_ArticleId",
                table: "UserArticleDislikes",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserArticleDislikes_Users_UserId",
                table: "UserArticleDislikes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserArticleLikes_Articles_ArticleId",
                table: "UserArticleLikes",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserArticleLikes_Users_UserId",
                table: "UserArticleLikes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserComment_Comments_CommentId",
                table: "UserComment",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserComment_Users_UserId",
                table: "UserComment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommentDislikes_Comments_CommentId",
                table: "UserCommentDislikes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommentDislikes_Users_UserId",
                table: "UserCommentDislikes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommentLikes_Comments_CommentId",
                table: "UserCommentLikes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommentLikes_Users_UserId",
                table: "UserCommentLikes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
