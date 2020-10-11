using AutoMapper;
using CleanEntityArchitecture.EntityModelServices;
using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;

namespace HackerNews.Api.Helpers.EntityServices.Base.ArticleServices
{
	public class ReadArticleService : ReadEntityService<Domain.Article, GetArticleModel>
	{
		public ReadArticleService(IMapper mapper, IReadEntityRepository<Article> readRepository) : base(mapper, readRepository)
		{
		}
	}
}
