using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Articles.Queries.GetArticle
{
	public class GetArticleQuery : IRequest<GetArticleModel>
	{
		public GetArticleQuery(int Id)
		{
			this.Id = Id;
		}

		public int Id { get; }
	}

	public class GetArticleHandler : DatabaseRequestHandler<GetArticleQuery, GetArticleModel>
	{
		private readonly IDeletedEntityPolicyValidator<Article> _deletedEntityValidator;

		public GetArticleHandler(IDeletedEntityPolicyValidator<Article> deletedEntityValidator,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_deletedEntityValidator = deletedEntityValidator;
		}

		public override async Task<GetArticleModel> Handle(GetArticleQuery request, CancellationToken cancellationToken)
		{
			Article article = await GetArticleById(request.Id);

			article = _deletedEntityValidator.ValidateEntity(article, Domain.Common.DeletedEntityPolicy.OWNER);

			return Mapper.Map<GetArticleModel>(article);
		}

		private async Task<Article> GetArticleById(int articleId)
		{
			var article = await UnitOfWork.Articles.GetEntityAsync(articleId);
			if (article == null) throw new NotFoundException();
			return article;
		}
	}
}
