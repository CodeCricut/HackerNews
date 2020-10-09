using AutoMapper;
using HackerNews.Domain.Models.Articles;
using HackerNews.EF.Repositories;

namespace HackerNews.Api.Helpers.EntityServices.Base.ArticleServices
{
	public class ReadArticleService : ReadEntityService<Domain.Article, GetArticleModel>
	{
		public ReadArticleService(IMapper mapper, IEntityRepository<Domain.Article> entityRepository) : base(mapper, entityRepository)
		{
		}
	}
}
