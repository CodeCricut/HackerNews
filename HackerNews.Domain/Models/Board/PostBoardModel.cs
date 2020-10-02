using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HackerNews.Domain.Models.Board
{
	public class PostBoardModel : PostEntityModel
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
	}
}
