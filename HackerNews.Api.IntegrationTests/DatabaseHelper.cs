using HackerNews.Domain;
using HackerNews.EF;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Api.IntegrationTests
{
	public static class DatabaseHelper
	{
		public static List<Article> Articles
		{
			get => new List<Article>
				{
					new Article { Id = 1, Title = "valid title 0", Text = "valid text 0", Karma = 0, Type = ArticleType.Meta },
					new Article { Id = 2, Title = "valid title 1", Text = "valid text 1", Karma = 1, Type = ArticleType.News },
					new Article { Id = 3, Title = "valid title 2", Text = "valid text 2", Karma = 2, Type = ArticleType.Opinion }
				};
		}

		public static List<Comment> Comments
		{
			get => new List<Comment>
				{
					new Comment { Id = 1, AuthorName = "valid author 0", Karma = 0, Text = "valid text 0"},
					new Comment { Id = 2, AuthorName = "valid author 1", Karma = 1, Text = "valid text 1"},
					new Comment { Id = 3, AuthorName = "valid author 2", Karma = 2, Text = "valid text 2"}
				};
		}

		public static void SeedDatabase(this HackerNewsContext context)
		{
			context.Articles.AddRange(Articles);
			context.SaveChanges();

			context.Comments.AddRange(Comments);
			context.SaveChanges();
		}

		public static void DisposeDatabase(this HackerNewsContext context)
		{
			var comments = context.Comments.ToList();
			var articles = context.Articles.ToList();

			context.RemoveRange(comments);
			context.RemoveRange(articles);

			context.SaveChanges();
		}
	}
}
