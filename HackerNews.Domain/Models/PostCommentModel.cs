using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HackerNews.Domain.Models
{
	public class PostCommentModel
	{
		[Required]
		public string AuthorName { get; set; }
		[Required]
		public string Text { get; set; }
		public int ParentCommentId { get; set; }
		public int ParentArticleId { get; set; }
	}
}
