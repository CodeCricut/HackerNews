using HackerNews.Domain.Common;
using HackerNews.Domain.Entities.JoinEntities;
using System;
using System.Collections.Generic;

namespace HackerNews.Domain.Entities
{
	public class Board : IDomainEntity
	{
		public int Id { get; set; }
		public bool Deleted { get; set; }

		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreateDate { get; set; }
		public User Creator { get; set; }
		public List<BoardUserModerator> Moderators { get; set; }
		public List<BoardUserSubscriber> Subscribers { get; set; }
		public List<Article> Articles { get; set; }
		public List<Comment> Comments { get; set; }

		public Image BoardImage { get; set; }
		

		public Board()
		{
			Moderators = new List<BoardUserModerator>();
			Subscribers = new List<BoardUserSubscriber>();
			Articles = new List<Article>();
		}
	}
}
