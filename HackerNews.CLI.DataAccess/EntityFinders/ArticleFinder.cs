using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.DataAccess.EntityFinders;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.CLI.EntityRepository
{
	public class ArticleFinder : EntityFinder<GetArticleModel, PostArticleModel>
	{
		public ArticleFinder(IEntityApiClient<PostArticleModel, GetArticleModel> entityApiClient, ILogger<EntityFinder<GetArticleModel, PostArticleModel>> logger) : base(entityApiClient, logger)
		{
		}
	}
}
