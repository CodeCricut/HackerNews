﻿using CleanEntityArchitecture.Domain;
using HackerNews.Domain.JoinEntities;
using System;
using System.Collections.Generic;

namespace HackerNews.Domain
{
	public class Board : DomainEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreateDate { get; set; }
		public User Creator { get; set; }
		public List<BoardUserModerator> Moderators { get; set; }
		public List<BoardUserSubscriber> Subscribers { get; set; }
		public List<Article> Articles { get; set; }

		public Board()
		{
			Moderators = new List<BoardUserModerator>();
			Subscribers = new List<BoardUserSubscriber>();
			Articles = new List<Article>();
		}
	}
}