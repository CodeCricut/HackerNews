using CleanEntityArchitecture.Domain;
using System;
using System.Collections.Generic;

namespace HackerNews.Domain.Models.Comments
{
	public class GetCommentModel : GetModelDto
	{
		public int UserId { get; set; }
		public string Text { get; set; }
		public string Url { get; set; }
		public int Karma { get; set; }
		public List<int> CommentIds { get; set; }
		public int ParentCommentId { get; set; }
		public int ParentArticleId { get; set; }

		public List<int> UsersLiked { get; set; }
		public List<int> UsersDisliked { get; set; }

		public DateTime PostDate { get; set; }

		public int BoardId { get; set; }
	}
}
