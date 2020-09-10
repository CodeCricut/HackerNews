using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using HackerNews.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Converters.Profiles
{
	public class CommentProfile : Profile
	{
		private readonly IEntityRepository<Comment> _commentRepository;
		private readonly IEntityRepository<Article> _articleRepository;

		public CommentProfile(IEntityRepository<Comment> commentRepository,
			IEntityRepository<Article> articleRepository)
		{
			_commentRepository = commentRepository;
			_articleRepository = articleRepository;

			CreateMap<Comment, Comment>();

			CreateMap<PostCommentModel, Comment>()
				.ForMember(c => c.ParentArticle, opt => opt.MapFrom(model => _articleRepository.GetEntityAsync(model.ParentArticleId)))
				.ForMember(c => c.ParentComment, opt => opt.MapFrom(model => _commentRepository.GetEntityAsync(model.ParentCommentId)));

			CreateMap<Comment, GetCommentModel>()
				.ForMember(cm => cm.ParentArticleId,
							o => o.MapFrom(c => c.ParentArticle.Id))
				.ForMember(cm => cm.ParentCommentId,
							o => o.MapFrom(c => c.ParentComment.Id))
				.ForMember(cm => cm.CommentIds,
							o => o.MapFrom(c => c.Comments.Select(c => c.Id))
						);
		}
	}
}
