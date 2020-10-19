﻿namespace HackerNews.Domain
{
	public class UserArticleLikes
	{
		public int UserId { get; set; }
		public User User { get; set; }
		public int ArticleId { get; set; }
		public Article Article { get; set; }
	}
}