using System;
using System.Collections.Generic;

namespace HackerNews.Domain
{
	public enum ArticleType
	{
		News,
		Opinion,
		Question,
		Meta
	}

	public class Article : DomainEntity
	{
		public ArticleType Type { get; set; }
		public string AuthorName { get; set; }
		public string Text { get; set; }
		public List<Comment> Comments { get; set; }
		public string Url { get; set; }
		public int Karma { get; set; }
		public string Title { get; set; }

		public Article()
		{
			Comments = new List<Comment>();
		}
	}
}
