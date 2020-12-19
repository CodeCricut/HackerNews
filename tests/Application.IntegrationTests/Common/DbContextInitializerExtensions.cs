using HackerNews.Domain.Entities;
using HackerNews.EF;

namespace Application.IntegrationTests
{
	public static class DbContextInitializerExtensions
	{
		/// <summary>
		/// Seed the database.
		/// </summary>
		/// <param name="context"></param>
		public static void InitializeForTests(this HackerNewsContext context)
		{
			User user = new User
			{
				FirstName = "user 1",
				LastName = "user 1",
				UserName = "user 1",
				Password = "user 1 password"
			};
			context.Users.Add(user);

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
