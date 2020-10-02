using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain
{
	public class UserCommentDislikes
	{
		public int UserId { get; set; }
		public User User { get; set; }
		public int CommentId { get; set; }
		public Comment Comment { get; set; }
	}
}
