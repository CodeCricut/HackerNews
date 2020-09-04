using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Converters.Profiles
{
	public class CommentProfile : Profile
	{
		public CommentProfile()
		{
			CreateMap<Comment, PostCommentModel>();
			CreateMap<PostCommentModel, Comment>();

			CreateMap<Comment, GetCommentModel>()
				.ForMember(cm => cm.ParentArticleId,
							o => o.MapFrom(c => c.ParentArticle.Id))
				.ForMember(cm => cm.ParentCommentId,
							o => o.MapFrom(c => c.ParentComment.Id));
		}
	}
}
