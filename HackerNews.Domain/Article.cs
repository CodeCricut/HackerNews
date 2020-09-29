using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackerNews.Domain
{
	public enum ArticleType
	{
		News,
		Opinion,
		Question,
		Meta
	}

	public class Article : DomainEntity
	{
		public ArticleType Type { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		public string Text { get; set; }
		[InverseProperty("ParentArticle")]
		public List<Comment> Comments { get; set; }
		public string Url { get; set; }
		public int Karma { get; set; }
		public string Title { get; set; }
		
		public List<UserArticle> UsersSaved { get; set; }

		public List<UserArticleLikes> UsersLiked { get; set; }
		public List<UserArticleDislikes> UsersDisliked { get; set; }

		public Article()
		{
			Comments = new List<Comment>();
			UsersSaved = new List<UserArticle>();
			UsersLiked = new List<UserArticleLikes>();
			UsersDisliked = new List<UserArticleDislikes>();
		}

		/// <summary>
		/// Shallow copy the given <paramref name="articleToClone"/> but add a reference to the given <paramref name="comments"/>.
		/// </summary>
		/// <param name="articleToClone"></param>
		public Article(Article articleToClone, List<Comment> comments)
		{
			Type = articleToClone.Type;
			User = articleToClone.User;
			Text = articleToClone.Text;
			Comments = comments;
			Url = articleToClone.Url;
			Karma = articleToClone.Karma;
			Title = articleToClone.Title;
		}

		/// <summary>
		/// Shallow copy the given <paramref name="articleToClone"/>.
		/// </summary>
		/// <param name="articleToClone"></param>
		public Article(Article articleToClone) : this(articleToClone, articleToClone.Comments) { }
	}
}
