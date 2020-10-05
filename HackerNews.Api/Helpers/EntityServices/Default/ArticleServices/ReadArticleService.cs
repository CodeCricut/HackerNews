using AutoMapper;
using HackerNews.Domain.Models.Articles;
using HackerNews.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.ArticleServices
{
	public class ReadArticleService : ReadEntityService<Domain.Article, GetArticleModel>
	{
		public ReadArticleService(IMapper mapper, IEntityRepository<Domain.Article> entityRepository) : base(mapper, entityRepository)
		{
		}
	}
}
