using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Articles;
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
		public GetArticleHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetArticleModel> Handle(GetArticleQuery request, CancellationToken cancellationToken)
		{
			var article = await UnitOfWork.Articles.GetEntityAsync(request.Id);
			if (article == null) throw new NotFoundException();

			return Mapper.Map<GetArticleModel>(article);
		}
	}
}
