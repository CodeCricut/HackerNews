using Microsoft.EntityFrameworkCore.Migrations;

namespace HackerNews.EF.Migrations
{
	public partial class initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Deleted = table.Column<bool>(nullable: false),
					FirstName = table.Column<string>(nullable: true),
					LastName = table.Column<string>(nullable: true),
					Username = table.Column<string>(nullable: true),
					Karma = table.Column<int>(nullable: false),
					Password = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				});

			//migrationBuilder.CreateTable(
			//    name: "Articles",
			//    columns: table => new
			//    {
			//        Id = table.Column<int>(nullable: false)
			//            .Annotation("SqlServer:Identity", "1, 1"),
			//        Deleted = table.Column<bool>(nullable: false),
			//        Type = table.Column<int>(nullable: false),
			//        AuthorId = table.Column<int>(nullable: true),
			//        Text = table.Column<string>(nullable: true),
			//        Url = table.Column<string>(nullable: true),
			//        Karma = table.Column<int>(nullable: false),
			//        Title = table.Column<string>(nullable: true)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK_Articles", x => x.Id);
			//        table.ForeignKey(
			//            name: "FK_Articles_Users_AuthorId",
			//            column: x => x.AuthorId,
			//            principalTable: "Users",
			//            principalColumn: "Id",
			//            onDelete: ReferentialAction.Restrict);
			//    });

			//migrationBuilder.CreateTable(
			//    name: "Comments",
			//    columns: table => new
			//    {
			//        Id = table.Column<int>(nullable: false)
			//            .Annotation("SqlServer:Identity", "1, 1"),
			//        Deleted = table.Column<bool>(nullable: false),
			//        AuthorId = table.Column<int>(nullable: true),
			//        Text = table.Column<string>(nullable: true),
			//        Url = table.Column<string>(nullable: true),
			//        Karma = table.Column<int>(nullable: false),
			//        ParentCommentId = table.Column<int>(nullable: true),
			//        ParentArticleId = table.Column<int>(nullable: true)
			//    },
			//    constraints: table =>
			//    {
			//        table.PrimaryKey("PK_Comments", x => x.Id);
			//        table.ForeignKey(
			//            name: "FK_Comments_Users_AuthorId",
			//            column: x => x.AuthorId,
			//            principalTable: "Users",
			//            principalColumn: "Id",
			//            onDelete: ReferentialAction.Restrict);
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
			//    });

			//migrationBuilder.CreateIndex(
			//    name: "IX_Articles_AuthorId",
			//    table: "Articles",
			//    column: "AuthorId");

			//migrationBuilder.CreateIndex(
			//    name: "IX_Comments_AuthorId",
			//    table: "Comments",
			//    column: "AuthorId");

			//migrationBuilder.CreateIndex(
			//    name: "IX_Comments_ParentArticleId",
			//    table: "Comments",
			//    column: "ParentArticleId");

			//migrationBuilder.CreateIndex(
			//    name: "IX_Comments_ParentCommentId",
			//    table: "Comments",
			//    column: "ParentCommentId");           
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Comments");

			migrationBuilder.DropTable(
				name: "Articles");

			migrationBuilder.DropTable(
				name: "Users");
		}
	}
}
