using AutoMapper;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Requests;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Articles.Commands.AddComment
{
	public class AddArticleCommentCommand : IRequest<GetArticleModel>
	{
		public AddArticleCommentCommand(int articleId, PostCommentModel postCommentModel)
		{
			ArticleId = articleId;
			PostCommentModel = postCommentModel;
		}

		public int ArticleId { get; }
		public PostCommentModel PostCommentModel { get; }
	}

	public class AddArticleCommentCommandHandler : DatabaseRequestHandler<AddArticleCommentCommand, GetArticleModel>
	{
		public AddArticleCommentCommandHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<GetArticleModel> Handle(AddArticleCommentCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var user = await Mediator.Send(new GetAuthenticatedUserQuery());
				if (user == null) throw new UnauthorizedException();

				var article = await UnitOfWork.Articles.GetEntityAsync(request.ArticleId);
				if (article == null) throw new NotFoundException();

				var comment = Mapper.Map<Comment>(request.PostCommentModel);
				comment.UserId = user.Id;
				comment.PostDate = DateTime.Now;

				var updatedArticle = await UnitOfWork.Articles.AddComment(request.ArticleId, comment);

				return Mapper.Map<GetArticleModel>(updatedArticle);
			}
		}
	}
}
