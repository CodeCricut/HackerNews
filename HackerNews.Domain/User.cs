﻿using CleanEntityArchitecture.Domain;
using HackerNews.Domain.JoinEntities;
using System;
using System.Collections.Generic;

namespace HackerNews.Domain
{
	public class User : BaseUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public int Karma { get; set; }
		// don't store in plaintext
		public string Password { get; set; }
		public List<Article> Articles { get; set; }
		public List<Comment> Comments { get; set; }

		public List<UserArticle> SavedArticles { get; set; }
		public List<UserComment> SavedComments { get; set; }

		public List<UserArticleLikes> LikedArticles { get; set; }
		public List<UserCommentLikes> LikedComments { get; set; }

		public List<UserArticleDislikes> DislikedArticles { get; set; }
		public List<UserCommentDislikes> DislikedComments { get; set; }

		public List<BoardUserModerator> BoardsModerating { get; set; }
		public List<BoardUserSubscriber> BoardsSubscribed { get; set; }

		public DateTime JoinDate { get; set; }

		public User()
		{
			Articles = new List<Article>();
			Comments = new List<Comment>();

			SavedArticles = new List<UserArticle>();
			SavedComments = new List<UserComment>();

			LikedArticles = new List<UserArticleLikes>();
			LikedComments = new List<UserCommentLikes>();

			DislikedArticles = new List<UserArticleDislikes>();
			DislikedComments = new List<UserCommentDislikes>();

			BoardsModerating = new List<BoardUserModerator>();
			BoardsSubscribed = new List<BoardUserSubscriber>();
		}
	}
}
