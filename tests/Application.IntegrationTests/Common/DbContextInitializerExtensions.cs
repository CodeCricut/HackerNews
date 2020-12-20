using HackerNews.Domain.Entities;
using HackerNews.EF;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
	public static class DbContextInitializerExtensions
	{
		/// <summary>
		/// Seed the database.
		/// </summary>
		/// <param name="context"></param>
		public static async Task InitializeForTestsAsync(this HackerNewsContext context, UserManager<User> userManager) 
		{
			User user = new User
			{
				FirstName = "user1",
				LastName = "user1",
				UserName = "user1",
			};
			IdentityResult result = await userManager.CreateAsync(user, password: user.UserName);
			if (!result.Succeeded) throw new Exception();
			context.SaveChanges();
			user = userManager.Users.FirstOrDefault(u => u.UserName == user.UserName);
			//context.Users.Add(user);

			Board board = new Board
			{
				Creator = user,
				Title = "board 1",
				Description = "board 1"
			};
			context.Boards.Add(board);

			Article article = new Article
			{
				User = user,
				Board = board,
				Title = "article 1",
				Text = "article 1",
				Type = ArticleType.Meta,
			};
			context.Articles.Add(article);

			Comment comment = new Comment
			{
				User = user,
				Board = board,
				ParentArticle = article,
				Text = "comment 1"
			};
			context.Comments.Add(comment);

			context.SaveChanges();
		}

		/// <summary>
		/// Remove all entities from the database.
		/// </summary>
		/// <param name="context"></param>
		public static void ClearDatabase(this HackerNewsContext context)
		{
			context.Users.RemoveRange(context.Users);
			context.Boards.RemoveRange(context.Boards);
			context.Articles.RemoveRange(context.Articles);
			context.Comments.RemoveRange(context.Comments);

			context.SaveChanges();
		}
	}
}
