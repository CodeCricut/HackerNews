using AutoMapper;
using HackerNews.Application.Common.Mappings;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Application.Common.Models.Users
{
	public class GetPrivateUserModel : GetModelDto, IMapFrom<User>
	{
		public GetPrivateUserModel()
		{

		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public int Karma { get; set; }
		// don't store in plaintext
		public string Password { get; set; }
		public List<int> ArticleIds { get; set; }
		public List<int> CommentIds { get; set; }

		public List<int> SavedArticles { get; set; }
		public List<int> SavedComments { get; set; }

		public List<int> LikedArticles { get; set; }
		public List<int> LikedComments { get; set; }

		public DateTime JoinDate { get; set; }

		public List<int> BoardsSubscribed { get; set; }
		public List<int> BoardsModerating { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<User, GetPrivateUserModel>()
				.ForMember(model => model.ArticleIds, user => user.MapFrom(u => u.Articles.Select(a => a.Id)))
				.ForMember(model => model.CommentIds, user => user.MapFrom(u => u.Comments.Select(c => c.Id)))

				.ForMember(model => model.SavedArticles, user => user.MapFrom(u => u.SavedArticles.Select(sa => sa.ArticleId)))
				.ForMember(model => model.SavedComments, user => user.MapFrom(u => u.SavedComments.Select(sc => sc.CommentId)))

				.ForMember(model => model.LikedArticles, user => user.MapFrom(u => u.LikedArticles.Select(la => la.ArticleId)))
				.ForMember(model => model.LikedComments, user => user.MapFrom(u => u.LikedComments.Select(lc => lc.CommentId)))

				.ForMember(model => model.BoardsModerating, user => user.MapFrom(u => u.BoardsModerating.Select(bm => bm.BoardId)))
				.ForMember(model => model.BoardsSubscribed, user => user.MapFrom(u => u.BoardsSubscribed.Select(bs => bs.BoardId)));
		}
	}
}
