﻿using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Models
{
	public class PostCommentModel : PostEntityModel
	{
		[Required]
		public int UserId { get; set; }
		[Required]
		public string Text { get; set; }
		public int ParentCommentId { get; set; }
		public int ParentArticleId { get; set; }

		public PostCommentModel()
		{

		}
	}
}
