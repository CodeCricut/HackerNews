using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HackerNews.Infrastructure.Migrations
{
	public partial class admin : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "AspNetRoles",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUsers",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserName = table.Column<string>(maxLength: 256, nullable: false),
					NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
					Email = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(nullable: false),
					PasswordHash = table.Column<string>(nullable: true),
					SecurityStamp = table.Column<string>(nullable: true),
					ConcurrencyStamp = table.Column<string>(nullable: true),
					PhoneNumber = table.Column<string>(nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(nullable: false),
					TwoFactorEnabled = table.Column<bool>(nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
					LockoutEnabled = table.Column<bool>(nullable: false),
					AccessFailedCount = table.Column<int>(nullable: false),
					Deleted = table.Column<bool>(nullable: false),
					FirstName = table.Column<string>(nullable: false),
					LastName = table.Column<string>(nullable: false),
					Karma = table.Column<int>(nullable: false),
					JoinDate = table.Column<DateTime>(nullable: false),
					AdminLevel = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUsers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetRoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RoleId = table.Column<int>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserClaims",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetUserClaims_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserLogins",
				columns: table => new
				{
					LoginProvider = table.Column<string>(nullable: false),
					ProviderKey = table.Column<string>(nullable: false),
					ProviderDisplayName = table.Column<string>(nullable: true),
					UserId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
					table.ForeignKey(
						name: "FK_AspNetUserLogins_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserRoles",
				columns: table => new
				{
					UserId = table.Column<int>(nullable: false),
					RoleId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserTokens",
				columns: table => new
				{
					UserId = table.Column<int>(nullable: false),
					LoginProvider = table.Column<string>(nullable: false),
					Name = table.Column<string>(nullable: false),
					Value = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
					table.ForeignKey(
						name: "FK_AspNetUserTokens_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

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
						name: "FK_Boards_AspNetUsers_CreatorId",
						column: x => x.CreatorId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Articles",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Type = table.Column<int>(nullable: false),
					UserId = table.Column<int>(nullable: false),
					Text = table.Column<string>(nullable: false),
					Url = table.Column<string>(nullable: true),
					Karma = table.Column<int>(nullable: false),
					Title = table.Column<string>(nullable: false),
					PostDate = table.Column<DateTime>(nullable: false),
					BoardId = table.Column<int>(nullable: false),
					Deleted = table.Column<bool>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Articles", x => x.Id);
					table.ForeignKey(
						name: "FK_Articles_Boards_BoardId",
						column: x => x.BoardId,
						principalTable: "Boards",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Articles_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
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
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_BoardUserModerator_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id");
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
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_BoardUserSubscriber_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "Comments",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Deleted = table.Column<bool>(nullable: false),
					UserId = table.Column<int>(nullable: false),
					Text = table.Column<string>(nullable: false),
					Url = table.Column<string>(nullable: true),
					Karma = table.Column<int>(nullable: false),
					BoardId = table.Column<int>(nullable: false),
					PostDate = table.Column<DateTime>(nullable: false),
					ParentCommentId = table.Column<int>(nullable: true),
					ParentArticleId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Comments", x => x.Id);
					table.ForeignKey(
						name: "FK_Comments_Boards_BoardId",
						column: x => x.BoardId,
						principalTable: "Boards",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Comments_Articles_ParentArticleId",
						column: x => x.ParentArticleId,
						principalTable: "Articles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Comments_Comments_ParentCommentId",
						column: x => x.ParentCommentId,
						principalTable: "Comments",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Comments_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

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
					ContentType = table.Column<string>(nullable: true),
					ArticleId = table.Column<int>(nullable: true),
					BoardId = table.Column<int>(nullable: true),
					UserId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Images", x => x.Id);
					table.ForeignKey(
						name: "FK_Images_Articles_ArticleId",
						column: x => x.ArticleId,
						principalTable: "Articles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Images_Boards_BoardId",
						column: x => x.BoardId,
						principalTable: "Boards",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Images_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

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
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_UserArticle_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id");
				});

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
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_UserArticleDislikes_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id");
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
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_UserArticleLikes_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id");
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
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_UserComment_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id");
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
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_UserCommentDislikes_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id");
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
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_UserCommentLikes_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id");
				});

			migrationBuilder.CreateIndex(
				name: "IX_Articles_BoardId",
				table: "Articles",
				column: "BoardId");

			migrationBuilder.CreateIndex(
				name: "IX_Articles_UserId",
				table: "Articles",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetRoleClaims_RoleId",
				table: "AspNetRoleClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles",
				column: "NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserClaims_UserId",
				table: "AspNetUserClaims",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserLogins_UserId",
				table: "AspNetUserLogins",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserRoles_RoleId",
				table: "AspNetUserRoles",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "EmailIndex",
				table: "AspNetUsers",
				column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "AspNetUsers",
				column: "NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL");

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

			migrationBuilder.CreateIndex(
				name: "IX_Comments_BoardId",
				table: "Comments",
				column: "BoardId");

			migrationBuilder.CreateIndex(
				name: "IX_Comments_ParentArticleId",
				table: "Comments",
				column: "ParentArticleId");

			migrationBuilder.CreateIndex(
				name: "IX_Comments_ParentCommentId",
				table: "Comments",
				column: "ParentCommentId");

			migrationBuilder.CreateIndex(
				name: "IX_Comments_UserId",
				table: "Comments",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Images_ArticleId",
				table: "Images",
				column: "ArticleId",
				unique: true,
				filter: "[ArticleId] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_Images_BoardId",
				table: "Images",
				column: "BoardId",
				unique: true,
				filter: "[BoardId] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_Images_UserId",
				table: "Images",
				column: "UserId",
				unique: true,
				filter: "[UserId] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_UserArticle_ArticleId",
				table: "UserArticle",
				column: "ArticleId");

			migrationBuilder.CreateIndex(
				name: "IX_UserArticleDislikes_ArticleId",
				table: "UserArticleDislikes",
				column: "ArticleId");

			migrationBuilder.CreateIndex(
				name: "IX_UserArticleLikes_ArticleId",
				table: "UserArticleLikes",
				column: "ArticleId");

			migrationBuilder.CreateIndex(
				name: "IX_UserComment_CommentId",
				table: "UserComment",
				column: "CommentId");

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
				name: "AspNetRoleClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserLogins");

			migrationBuilder.DropTable(
				name: "AspNetUserRoles");

			migrationBuilder.DropTable(
				name: "AspNetUserTokens");

			migrationBuilder.DropTable(
				name: "BoardUserModerator");

			migrationBuilder.DropTable(
				name: "BoardUserSubscriber");

			migrationBuilder.DropTable(
				name: "Images");

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
				name: "AspNetRoles");

			migrationBuilder.DropTable(
				name: "Comments");

			migrationBuilder.DropTable(
				name: "Articles");

			migrationBuilder.DropTable(
				name: "Boards");

			migrationBuilder.DropTable(
				name: "AspNetUsers");
		}
	}
}
