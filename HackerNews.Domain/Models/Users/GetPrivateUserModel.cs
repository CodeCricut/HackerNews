using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Models.Users
{
	public class GetPrivateUserModel : GetEntityModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public int Karma { get; set; }
		// don't store in plaintext
		public string Password { get; set; }
		public List<int> ArticleIds { get; set; }
		public List<int> CommentIds { get; set; }

		public List<int> SavedArticles { get; set; }
		public List<int> SavedComments { get; set; }
	}
}
