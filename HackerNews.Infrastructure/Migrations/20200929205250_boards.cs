using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HackerNews.EF.Migrations
{
	public partial class boards : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "BoardId",
				table: "Articles",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.CreateTable(
				name: "Boards",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Deleted = table.Column<bool>(nullable: false),
					Title = table.Column<string>(nullable: true),
					Description = table.Column<string>(nullable: true),
					CreateDate = table.Column<DateTime>(nullable: false),
					CreatorId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Boards", x => x.Id);
					table.ForeignKey(
						name: "FK_Boards_Users_CreatorId",
						column: x => x.CreatorId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "BoardUserModerator",
				columns: table => new
				{
					BoardId = table.Column<int>(nullable: false),
					UserId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_BoardUserModerator", x => new { x.UserId, x.BoardId });
					table.ForeignKey(
						name: "FK_BoardUserModerator_Boards_BoardId",
						column: x => x.BoardId,
						principalTable: "Boards",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_BoardUserModerator_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "BoardUserSubscriber",
				columns: table => new
				{
					BoardId = table.Column<int>(nullable: false),
					UserId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_BoardUserSubscriber", x => new { x.UserId, x.BoardId });
					table.ForeignKey(
						name: "FK_BoardUserSubscriber_Boards_BoardId",
						column: x => x.BoardId,
						principalTable: "Boards",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_BoardUserSubscriber_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Articles_BoardId",
				table: "Articles",
				column: "BoardId");

			migrationBuilder.CreateIndex(
				name: "IX_Boards_CreatorId",
				table: "Boards",
				column: "CreatorId");

			migrationBuilder.CreateIndex(
				name: "IX_BoardUserModerator_BoardId",
				table: "BoardUserModerator",
				column: "BoardId");

			migrationBuilder.CreateIndex(
				name: "IX_BoardUserSubscriber_BoardId",
				table: "BoardUserSubscriber",
				column: "BoardId");

			migrationBuilder.AddForeignKey(
				name: "FK_Articles_Boards_BoardId",
				table: "Articles",
				column: "BoardId",
				principalTable: "Boards",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Articles_Boards_BoardId",
				table: "Articles");

			migrationBuilder.DropTable(
				name: "BoardUserModerator");

			migrationBuilder.DropTable(
				name: "BoardUserSubscriber");

			migrationBuilder.DropTable(
				name: "Boards");

			migrationBuilder.DropIndex(
				name: "IX_Articles_BoardId",
				table: "Articles");

			migrationBuilder.DropColumn(
				name: "BoardId",
				table: "Articles");
		}
	}
}
