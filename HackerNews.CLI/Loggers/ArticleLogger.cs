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
			string printString = $"ARTICLE {article.Id}: Title={article.Title}; Text={article.Text}; PostDate{article.PostDate}";
			_logger.LogInformation(printString);
		}

		public void LogEntityPage(PaginatedList<GetArticleModel> articlePage)
		{
			_logger.LogInformation($"Article Page: PageSize{articlePage.PageSize}; {articlePage.PageIndex} / {articlePage.TotalPages}");
			_logger.LogInformation("Id\tTitle\tText");

			foreach (var article in articlePage.Items)
			{
				_logger.LogInformation($"{article.Id}\t{article.Title}\t{article.Text}");
			}
		}
	}
}
