using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Loggers
{
	public class ArticleLogger : EntityLogger<GetArticleModel>, IEntityLogger<GetArticleModel>
	{
		public ArticleLogger(ILogger<EntityLogger<GetArticleModel>> logger, IEntityReader<GetArticleModel> entityReader) : base(logger, entityReader)
		{
		}

		protected override string GetEntityName()
			=> "Article";

		protected override string GetEntityPlural()
			=> "Articles";
	}
}
