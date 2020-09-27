using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Models
{
	public class GetPublicUserModel : GetEntityModel
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public int Karma { get; set; }
		public bool Deleted { get; set; }

		public List<int> ArticleIds { get; set; }
		public List<int> CommentIds { get; set; }
	}
}
