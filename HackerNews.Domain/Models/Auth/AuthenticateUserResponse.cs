using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerNews.Domain.Models.Auth
{
	public class AuthenticateUserResponse : GetEntityModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public int Karma { get; set; }
		public bool Deleted { get; set; }
		public DateTime JoinDate { get; set; }

		public List<int> ArticleIds { get; set; }
		public List<int> CommentIds { get; set; }

		public string Token { get; set; }

		// TODO: for deserializing 
		public AuthenticateUserResponse()
		{

		}

		public AuthenticateUserResponse(User user, string token)
		{
			// could be improved with auto mapper
			Id = user.Id;
			FirstName = user.FirstName;
			LastName = user.LastName;
			Username = user.Username;
			Karma = user.Karma;
			Deleted = user.Deleted;

			ArticleIds = user.Articles.Select(a => a.Id).ToList();
			CommentIds = user.Comments.Select(c => c.Id).ToList();

			Token = token;

			JoinDate = user.JoinDate;
		}
	}
}
