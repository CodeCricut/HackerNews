﻿using HackerNews.Domain.Common;

namespace HackerNews.Domain.Entities
{
	public class Image : DomainEntity
	{
		public string ImageTitle { get; set; }
		public string ImageDescription { get; set; }
		public byte[] ImageData { get; set; }
		public string ContentType { get; set; }

		public int? ArticleId { get; set; }
		public Article Article { get; set; }
		public int? BoardId { get; set; }
		public Board Board { get; set; }
		public int? UserId { get; set; }
		public User User { get; set; }
	}
}
