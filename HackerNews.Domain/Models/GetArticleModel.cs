using System.Collections.Generic;

namespace HackerNews.Domain.Models
{
	public class GetArticleModel : GetEntityModel
	{
		public int Id { get; set; }
		public bool Deleted { get; set; }
		public ArticleType Type { get; set; }
		public int AuthorId { get; set; }
		public string Text { get; set; }
		public List<int> CommentIds { get; set; }
		public string Url { get; set; }
		public int Karma { get; set; }
		public string Title { get; set; }
	}
}
