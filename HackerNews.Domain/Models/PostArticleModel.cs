using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HackerNews.Domain.Models
{
	public class PostArticleModel
	{
		[Required]
		public string Type { get; set; }
		[Required]
		public string AuthorName { get; set; }
		[Required]
		public string Text { get; set; }
		[Required]
		public string Title { get; set; }

		// needed for model binding
		public PostArticleModel()
		{

		}
	}
}
