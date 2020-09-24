using Microsoft.EntityFrameworkCore.Migrations;

namespace HackerNews.EF.Migrations
{
	public partial class initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Articles",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Deleted = table.Column<bool>(nullable: false),
					Type = table.Column<int>(nullable: false),
					AuthorName = table.Column<string>(nullable: true),
					Text = table.Column<string>(nullable: true),
					Url = table.Column<string>(nullable: true),
					Karma = table.Column<int>(nullable: false),
					Title = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Articles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Comments",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Deleted = table.Column<bool>(nullable: false),
					AuthorName = table.Column<string>(nullable: true),
					Text = table.Column<string>(nullable: true),
					Url = table.Column<string>(nullable: true),
					Karma = table.Column<int>(nullable: false),
					ParentCommentId = table.Column<int>(nullable: true),
					ArticleId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Comments", x => x.Id);
					table.ForeignKey(
						name: "FK_Comments_Articles_ArticleId",
						column: x => x.ArticleId,
						principalTable: "Articles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Comments_Comments_ParentCommentId",
						column: x => x.ParentCommentId,
						principalTable: "Comments",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Comments_ArticleId",
				table: "Comments",
				column: "ArticleId");

			migrationBuilder.CreateIndex(
				name: "IX_Comments_ParentCommentId",
				table: "Comments",
				column: "ParentCommentId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Comments");

			migrationBuilder.DropTable(
				name: "Articles");
		}
	}
}
