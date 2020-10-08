using System;
using System.Collections.Generic;

namespace HackerNews.Domain.Models.Users
{
	public class GetPrivateUserModel : GetEntityModel
	{
		public GetPrivateUserModel()
		{

		}

		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public int Karma { get; set; }
		public bool Deleted { get; set; }
		// don't store in plaintext
		public string Password { get; set; }
		public List<int> ArticleIds { get; set; }
		public List<int> CommentIds { get; set; }

		public List<int> SavedArticles { get; set; }
		public List<int> SavedComments { get; set; }

		public List<int> LikedArticles { get; set; }
		public List<int> LikedComments { get; set; }

		public DateTime JoinDate { get; set; }

		public string JwtToken { get; set; }

		public List<int> BoardsSubscribed { get; set; }
		public List<int> BoardsModerating { get; set; }
	}
}
