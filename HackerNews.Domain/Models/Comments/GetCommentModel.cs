using System.Collections.Generic;

namespace HackerNews.Domain.Models.Comments
{
	public class GetCommentModel : GetEntityModel
	{
		public int Id { get; set; }
		public bool Deleted { get; set; }
		public int UserId { get; set; }
		public string Text { get; set; }
		public string Url { get; set; }
		public int Karma { get; set; }
		public List<int> CommentIds { get; set; }
		public int ParentCommentId { get; set; }
		public int ParentArticleId { get; set; }
	}
}
