using AutoMapper;
using HackerNews.Application.Common.Mappings;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Application.Common.Models.Articles
{
	public class GetArticleModel : GetModelDto, IMapFrom<Article>
	{
		public ArticleType Type { get; set; }
		public int UserId { get; set; }
		public string Text { get; set; }
		public List<int> CommentIds { get; set; }
		public string Url { get; set; }
		public int Karma { get; set; }
		public string Title { get; set; }
		public List<int> UsersLiked { get; set; }
		public List<int> UsersDisliked { get; set; }
		public DateTime PostDate { get; set; }
		public int BoardId { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Article, GetArticleModel>()
				.ForMember(model => model.CommentIds, article => article.MapFrom(a => a.Comments.Select(a => a.Id)))
				.ForMember(model => model.UsersLiked, article => article.MapFrom(a => a.UsersLiked.Select(ul => ul.UserId)))
				.ForMember(model => model.UsersDisliked, article => article.MapFrom(a => a.UsersDisliked.Select(ud => ud.UserId)));
		}
	}
}
