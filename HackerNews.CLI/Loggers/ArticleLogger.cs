using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Loggers
{
	public class ArticleLogger : IEntityLogger<GetArticleModel>
	{
		private readonly ILogger<ArticleLogger> _logger;

		public ArticleLogger(ILogger<ArticleLogger> logger)
		{
			_logger = logger;
		}

		public void LogEntity(GetArticleModel article)
		{
			// TODO
			string printString = $"ARTICLE {article.Id}: Title={article.Title}; Text={article.Text}; PostDate{article.PostDate}";
			_logger.LogInformation(printString);
		}

		public void LogEntityPage(PaginatedList<GetArticleModel> articlePage)
		{
			// TODO:
			_logger.LogInformation(articlePage.PageSize.ToString());
		}
	}
}
