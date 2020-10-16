using CleanEntityArchitecture.Domain;
using System;
using System.Collections.Generic;

namespace HackerNews.Domain.Models.Users
{
	public class GetPublicUserModel : GetModelDto
	{
		public string Username { get; set; }
		public int Karma { get; set; }
		public bool Deleted { get; set; }

		public List<int> ArticleIds { get; set; }
		public List<int> CommentIds { get; set; }

		public DateTime JoinDate { get; set; }
	}
}
