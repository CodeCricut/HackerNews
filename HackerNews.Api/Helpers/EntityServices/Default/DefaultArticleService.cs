using AutoMapper;
using HackerNews.Domain;
using HackerNews.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Default
{
	public class DefaultArticleService : ArticleService
	{
		public DefaultArticleService(IEntityRepository<Article> entityRepository, IMapper mapper) 
			: base(entityRepository, mapper)
		{
		}
	}
}
