using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Requests;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Articles.Commands.VoteArticle
{
	public class VoteArticleCommand : IRequest<GetArticleModel>
	{
		public VoteArticleCommand(int articleId, bool upvote)
		{
			ArticleId = articleId;
			Upvote = upvote;
		}

		public int ArticleId { get; }
		public bool Upvote { get; }
	}

	public class VoteArticleCommandHandler : DatabaseRequestHandler<VoteArticleCommand, GetArticleModel>
	{
		public VoteArticleCommandHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<GetArticleModel> Handle(VoteArticleCommand request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var user = await Mediator.Send(new GetAuthenticatedUserQuery());
				if (user == null) throw new UnauthorizedException();

				var article = await UnitOfWork.Articles.GetEntityAsync(request.ArticleId);
				if (article == null) throw new NotFoundException();

				var updatedArticle = await UnitOfWork.Articles.VoteArticle(request.ArticleId, request.Upvote);

				UnitOfWork.SaveChanges();

				return Mapper.Map<GetArticleModel>(updatedArticle);
			}
		}
	}
}
