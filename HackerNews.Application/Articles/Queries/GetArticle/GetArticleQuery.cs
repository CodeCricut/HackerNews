using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
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
		public GetArticleHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<GetArticleModel> Handle(GetArticleQuery request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var article = await UnitOfWork.Articles.GetEntityAsync(request.Id);
				if (article == null) throw new NotFoundException();

				return Mapper.Map<GetArticleModel>(article);
			}
		}
	}
}
