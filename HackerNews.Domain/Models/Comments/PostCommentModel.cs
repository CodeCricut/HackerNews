﻿using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Models.Comments
{
	public class PostCommentModel : PostEntityModel
	{
		[Required]
		public string Text { get; set; }
		[Required]
		[Range(1, int.MaxValue)]
		public int BoardId { get; set; }
		[Range(0, int.MaxValue)]
		public int ParentCommentId { get; set; }
		[Range(0, int.MaxValue)]
		public int ParentArticleId { get; set; }

		public PostCommentModel()
		{

		}
	}
}
