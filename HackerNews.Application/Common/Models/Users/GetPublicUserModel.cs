using AutoMapper;
using HackerNews.Application.Common.Mappings;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Application.Common.Models.Users
{
	public class GetPublicUserModel : GetModelDto, IMapFrom<User>
	{
		public string Username { get; set; }
		public int Karma { get; set; }

		public List<int> ArticleIds { get; set; }
		public List<int> CommentIds { get; set; }

		public DateTime JoinDate { get; set; }
		public bool Deleted { get; set; }


		public void Mapping(Profile profile)
		{
			profile.CreateMap<User, GetPublicUserModel>()
				.ForMember(model => model.ArticleIds, user => user.MapFrom(u => u.Articles.Select(a => a.Id)))
				.ForMember(model => model.CommentIds, user => user.MapFrom(u => u.Comments.Select(c => c.Id)));
		}
	}
}
