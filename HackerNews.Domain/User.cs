using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain
{
	public class User : DomainEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public int Karma { get; set; }
		// don't store in plaintext
		public string Password { get; set; }
		public List<Article> Articles { get; set; }
		public List<Comment> Comments { get; set; }

		public List<UserArticle> SavedArticles { get; set; }
		public List<UserComment> SavedComments { get; set; }

		public User()
		{
			Articles = new List<Article>();
			Comments = new List<Comment>();
		}
	}
}
