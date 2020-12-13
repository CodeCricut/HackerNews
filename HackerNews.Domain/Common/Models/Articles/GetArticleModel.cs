using AutoMapper;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Domain.Common.Models.Articles
{
	public class GetArticleModel : GetModelDto, IMapFrom<Article>
	{
		public ArticleType Type { get; set; }
		public int UserId { get; set; }
		public string Text { get; set; }
		public List<int> CommentIds { get; set; } = new List<int>();
		public string Url { get; set; }
		public int Karma { get; set; }
		public string Title { get; set; }
		public List<int> UsersLiked { get; set; } = new List<int>();
		public List<int> UsersDisliked { get; set; } = new List<int>();
		public DateTime PostDate { get; set; }
		public int BoardId { get; set; }
		public bool Deleted { get; set; }

		public int AssociatedImageId { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Article, GetArticleModel>()
				.ForMember(model => model.CommentIds, article => article.MapFrom(a => a.Comments.Select(a => a.Id)))
				.ForMember(model => model.UsersLiked, article => article.MapFrom(a => a.UsersLiked.Select(ul => ul.UserId)))
				.ForMember(model => model.UsersDisliked, article => article.MapFrom(a => a.UsersDisliked.Select(ud => ud.UserId)))
				.ForMember(model => model.AssociatedImageId, article => article.MapFrom(a => a.AssociatedImage.Id));
		}
	}
}
