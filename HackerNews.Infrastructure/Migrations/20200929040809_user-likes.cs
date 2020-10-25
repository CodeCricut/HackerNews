using Microsoft.EntityFrameworkCore.Migrations;

namespace HackerNews.EF.Migrations
{
	public partial class userlikes : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "UserArticleDislikes",
				columns: table => new
				{
					UserId = table.Column<int>(nullable: false),
					ArticleId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserArticleDislikes", x => new { x.UserId, x.ArticleId });
					table.ForeignKey(
						name: "FK_UserArticleDislikes_Articles_ArticleId",
						column: x => x.ArticleId,
						principalTable: "Articles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_UserArticleDislikes_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "UserArticleLikes",
				columns: table => new
				{
					UserId = table.Column<int>(nullable: false),
					ArticleId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserArticleLikes", x => new { x.UserId, x.ArticleId });
					table.ForeignKey(
						name: "FK_UserArticleLikes_Articles_ArticleId",
						column: x => x.ArticleId,
						principalTable: "Articles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_UserArticleLikes_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "UserCommentDislikes",
				columns: table => new
				{
					UserId = table.Column<int>(nullable: false),
					CommentId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserCommentDislikes", x => new { x.UserId, x.CommentId });
					table.ForeignKey(
						name: "FK_UserCommentDislikes_Comments_CommentId",
						column: x => x.CommentId,
						principalTable: "Comments",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_UserCommentDislikes_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "UserCommentLikes",
				columns: table => new
				{
					UserId = table.Column<int>(nullable: false),
					CommentId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserCommentLikes", x => new { x.UserId, x.CommentId });
					table.ForeignKey(
						name: "FK_UserCommentLikes_Comments_CommentId",
						column: x => x.CommentId,
						principalTable: "Comments",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_UserCommentLikes_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_UserArticleDislikes_ArticleId",
				table: "UserArticleDislikes",
				column: "ArticleId");

			migrationBuilder.CreateIndex(
				name: "IX_UserArticleLikes_ArticleId",
				table: "UserArticleLikes",
				column: "ArticleId");

			migrationBuilder.CreateIndex(
				name: "IX_UserCommentDislikes_CommentId",
				table: "UserCommentDislikes",
				column: "CommentId");

			migrationBuilder.CreateIndex(
				name: "IX_UserCommentLikes_CommentId",
				table: "UserCommentLikes",
				column: "CommentId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "UserArticleDislikes");

			migrationBuilder.DropTable(
				name: "UserArticleLikes");

			migrationBuilder.DropTable(
				name: "UserCommentDislikes");

			migrationBuilder.DropTable(
				name: "UserCommentLikes");
		}
	}
}
