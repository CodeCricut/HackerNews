using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using System.Linq;

namespace HackerNews.Api.Converters.Profiles
{
	public class CommentProfile : Profile
	{
		public CommentProfile()
		{
			CreateMap<Comment, Comment>();

			CreateMap<PostCommentModel, Comment>()
				.ForMember(c => c.ParentArticleId, opt => opt
					.MapFrom(model => model.ParentArticleId != 0 ? model.ParentArticleId : (int?) null))
				.ForMember(c => c.ParentCommentId, opt => opt
					.MapFrom(model => model.ParentCommentId != 0 ? model.ParentCommentId : (int?) null));
				//.ForMember(c => c.ParentArticleId, opt => opt.MapFrom(model => model.ParentArticleId))
				//.ForMember(c => c.ParentArticle.Id, opt => opt.MapFrom(model => model.ParentArticleId))
				//.ForMember(c => c.ParentComment.Id, opt => opt.MapFrom(model => model.ParentCommentId));

			CreateMap<Comment, GetCommentModel>()
				//.ForMember(cm => cm.ParentArticleId,
				//			o => o.MapFrom(c => c.ParentArticle.Id))
				//.ForMember(cm => cm.ParentCommentId,
				//			o => o.MapFrom(c => c.ParentComment.Id))
				.ForMember(cm => cm.CommentIds,
							o => o.MapFrom(c => c.ChildComments.Select(c => c.Id))
						);
		}
	}
}
