using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackerNews.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Deleted = table.Column<bool>(nullable: false),
            //        Username = table.Column<string>(nullable: false),
            //        Password = table.Column<string>(nullable: false),
            //        FirstName = table.Column<string>(nullable: false),
            //        LastName = table.Column<string>(nullable: false),
            //        Karma = table.Column<int>(nullable: false),
            //        JoinDate = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Boards",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Deleted = table.Column<bool>(nullable: false),
            //        Title = table.Column<string>(nullable: true),
            //        Description = table.Column<string>(nullable: true),
            //        CreateDate = table.Column<DateTime>(nullable: false),
            //        CreatorId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Boards", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Boards_Users_CreatorId",
            //            column: x => x.CreatorId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Articles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Deleted = table.Column<bool>(nullable: false),
            //        Type = table.Column<int>(nullable: false),
            //        UserId = table.Column<int>(nullable: false),
            //        Text = table.Column<string>(nullable: false),
            //        Url = table.Column<string>(nullable: true),
            //        Karma = table.Column<int>(nullable: false),
            //        Title = table.Column<string>(nullable: false),
            //        PostDate = table.Column<DateTime>(nullable: false),
            //        BoardId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Articles", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Articles_Boards_BoardId",
            //            column: x => x.BoardId,
            //            principalTable: "Boards",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Articles_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "BoardUserModerator",
            //    columns: table => new
            //    {
            //        BoardId = table.Column<int>(nullable: false),
            //        UserId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BoardUserModerator", x => new { x.UserId, x.BoardId });
            //        table.ForeignKey(
            //            name: "FK_BoardUserModerator_Boards_BoardId",
            //            column: x => x.BoardId,
            //            principalTable: "Boards",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_BoardUserModerator_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "BoardUserSubscriber",
            //    columns: table => new
            //    {
            //        BoardId = table.Column<int>(nullable: false),
            //        UserId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BoardUserSubscriber", x => new { x.UserId, x.BoardId });
            //        table.ForeignKey(
            //            name: "FK_BoardUserSubscriber_Boards_BoardId",
            //            column: x => x.BoardId,
            //            principalTable: "Boards",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_BoardUserSubscriber_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Comments",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Deleted = table.Column<bool>(nullable: false),
            //        UserId = table.Column<int>(nullable: false),
            //        Text = table.Column<string>(nullable: false),
            //        Url = table.Column<string>(nullable: true),
            //        Karma = table.Column<int>(nullable: false),
            //        BoardId = table.Column<int>(nullable: false),
            //        PostDate = table.Column<DateTime>(nullable: false),
            //        ParentCommentId = table.Column<int>(nullable: true),
            //        ParentArticleId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Comments", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Comments_Boards_BoardId",
            //            column: x => x.BoardId,
            //            principalTable: "Boards",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Comments_Articles_ParentArticleId",
            //            column: x => x.ParentArticleId,
            //            principalTable: "Articles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Comments_Comments_ParentCommentId",
            //            column: x => x.ParentCommentId,
            //            principalTable: "Comments",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Comments_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserArticle",
            //    columns: table => new
            //    {
            //        UserId = table.Column<int>(nullable: false),
            //        ArticleId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserArticle", x => new { x.UserId, x.ArticleId });
            //        table.ForeignKey(
            //            name: "FK_UserArticle_Articles_ArticleId",
            //            column: x => x.ArticleId,
            //            principalTable: "Articles",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_UserArticle_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserArticleDislikes",
            //    columns: table => new
            //    {
            //        UserId = table.Column<int>(nullable: false),
            //        ArticleId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserArticleDislikes", x => new { x.UserId, x.ArticleId });
            //        table.ForeignKey(
            //            name: "FK_UserArticleDislikes_Articles_ArticleId",
            //            column: x => x.ArticleId,
            //            principalTable: "Articles",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_UserArticleDislikes_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserArticleLikes",
            //    columns: table => new
            //    {
            //        UserId = table.Column<int>(nullable: false),
            //        ArticleId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserArticleLikes", x => new { x.UserId, x.ArticleId });
            //        table.ForeignKey(
            //            name: "FK_UserArticleLikes_Articles_ArticleId",
            //            column: x => x.ArticleId,
            //            principalTable: "Articles",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_UserArticleLikes_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserComment",
            //    columns: table => new
            //    {
            //        UserId = table.Column<int>(nullable: false),
            //        CommentId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserComment", x => new { x.UserId, x.CommentId });
            //        table.ForeignKey(
            //            name: "FK_UserComment_Comments_CommentId",
            //            column: x => x.CommentId,
            //            principalTable: "Comments",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_UserComment_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserCommentDislikes",
            //    columns: table => new
            //    {
            //        UserId = table.Column<int>(nullable: false),
            //        CommentId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserCommentDislikes", x => new { x.UserId, x.CommentId });
            //        table.ForeignKey(
            //            name: "FK_UserCommentDislikes_Comments_CommentId",
            //            column: x => x.CommentId,
            //            principalTable: "Comments",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_UserCommentDislikes_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserCommentLikes",
            //    columns: table => new
            //    {
            //        UserId = table.Column<int>(nullable: false),
            //        CommentId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserCommentLikes", x => new { x.UserId, x.CommentId });
            //        table.ForeignKey(
            //            name: "FK_UserCommentLikes_Comments_CommentId",
            //            column: x => x.CommentId,
            //            principalTable: "Comments",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_UserCommentLikes_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Articles_BoardId",
            //    table: "Articles",
            //    column: "BoardId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Articles_UserId",
            //    table: "Articles",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Boards_CreatorId",
            //    table: "Boards",
            //    column: "CreatorId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_BoardUserModerator_BoardId",
            //    table: "BoardUserModerator",
            //    column: "BoardId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_BoardUserSubscriber_BoardId",
            //    table: "BoardUserSubscriber",
            //    column: "BoardId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Comments_BoardId",
            //    table: "Comments",
            //    column: "BoardId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Comments_ParentArticleId",
            //    table: "Comments",
            //    column: "ParentArticleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Comments_ParentCommentId",
            //    table: "Comments",
            //    column: "ParentCommentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Comments_UserId",
            //    table: "Comments",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserArticle_ArticleId",
            //    table: "UserArticle",
            //    column: "ArticleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserArticleDislikes_ArticleId",
            //    table: "UserArticleDislikes",
            //    column: "ArticleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserArticleLikes_ArticleId",
            //    table: "UserArticleLikes",
            //    column: "ArticleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserComment_CommentId",
            //    table: "UserComment",
            //    column: "CommentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserCommentDislikes_CommentId",
            //    table: "UserCommentDislikes",
            //    column: "CommentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserCommentLikes_CommentId",
            //    table: "UserCommentLikes",
            //    column: "CommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardUserModerator");

            migrationBuilder.DropTable(
                name: "BoardUserSubscriber");

            migrationBuilder.DropTable(
                name: "UserArticle");

            migrationBuilder.DropTable(
                name: "UserArticleDislikes");

            migrationBuilder.DropTable(
                name: "UserArticleLikes");

            migrationBuilder.DropTable(
                name: "UserComment");

            migrationBuilder.DropTable(
                name: "UserCommentDislikes");

            migrationBuilder.DropTable(
                name: "UserCommentLikes");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
