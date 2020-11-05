using AutoMapper;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Domain.Common.Models.Comments
{
	public class GetCommentModel : GetModelDto, IMapFrom<Comment>
	{
		public int UserId { get; set; }
		public string Text { get; set; }
		public string Url { get; set; }
		public int Karma { get; set; }
		public List<int> CommentIds { get; set; } = new List<int>();
		public int ParentCommentId { get; set; }
		public int ParentArticleId { get; set; }
		public bool Deleted { get; set; }


		public List<int> UsersLiked { get; set; } = new List<int>();
		public List<int> UsersDisliked { get; set; } = new List<int>();

		public DateTime PostDate { get; set; }

		public int BoardId { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Comment, GetCommentModel>()
				.ForMember(cm => cm.CommentIds,
							o => o.MapFrom(c => c.ChildComments.Select(c => c.Id)))
				.ForMember(cm => cm.UsersLiked,
							o => o.MapFrom(c => c.UsersLiked.Select(ul => ul.UserId)))
				.ForMember(cm => cm.UsersDisliked,
							o => o.MapFrom(c => c.UsersDisliked.Select(ul => ul.UserId)));
		}
	}
}
