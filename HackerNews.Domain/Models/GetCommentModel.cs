﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HackerNews.Domain.Models
{
	public class GetCommentModel : GetEntityModel
	{
		public int Id { get; set; }
		public bool Deleted { get; set; }
		public string AuthorName { get; set; }
		public string Text { get; set; }
		public string Url { get; set; }
		public int Karma { get; set; }
		public List<Comment> Comments { get; set; }
		public int ParentCommentId { get; set; }
		public int ParentArticleId { get; set; }
	}
}
