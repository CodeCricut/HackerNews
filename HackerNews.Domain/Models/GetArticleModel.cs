using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Models
{
	public class GetArticleModel
	{
		public int Id { get; set; }
		public bool Deleted { get; set; }
		public ArticleType Type { get; set; }
		public string AuthorName { get; set; }
		public string Text { get; set; }
		// comment IDs
		public IEnumerable<int> Comments { get; set; }
		public string Url { get; set; }
		public int Karma { get; set; }
		public string Title { get; set; }
	}
}
