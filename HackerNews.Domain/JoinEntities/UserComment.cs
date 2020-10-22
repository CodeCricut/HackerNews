﻿namespace HackerNews.Domain.JoinEntities
{
	public class UserComment
	{
		public int UserId { get; set; }
		public User User { get; set; }
		public int CommentId { get; set; }
		public Comment Comment { get; set; }
	}
}
