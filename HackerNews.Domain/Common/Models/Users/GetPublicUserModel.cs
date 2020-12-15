using AutoMapper;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Domain.Common.Models.Users
{
	public class GetPublicUserModel : GetModelDto, IMapFrom<User>
	{
		public string Username { get; set; } 
		public int Karma { get; set; }

		public List<int> ArticleIds { get; set; } = new List<int>();
		public List<int> CommentIds { get; set; } = new List<int>();

		public DateTime JoinDate { get; set; }
		public bool Deleted { get; set; }

		public int ProfileImageId { get; set; }


		public void Mapping(Profile profile)
		{
			profile.CreateMap<User, GetPublicUserModel>()
				.ForMember(model => model.ArticleIds, user => user.MapFrom(u => u.Articles.Select(a => a.Id)))
				.ForMember(model => model.CommentIds, user => user.MapFrom(u => u.Comments.Select(c => c.Id)))

				.ForMember(model => model.ProfileImageId, user => user.MapFrom(u => u.ProfileImage.Id));
		}
	}
}
