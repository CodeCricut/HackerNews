using Microsoft.EntityFrameworkCore.Migrations;

namespace HackerNews.EF.Migrations
{
    public partial class boardIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoardId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BoardId",
                table: "Comments",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Boards_BoardId",
                table: "Comments",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Boards_BoardId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BoardId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Comments");
        }
    }
}
