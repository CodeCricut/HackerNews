using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Verbs.GetArticles
{
	public interface IGetArticleProcessor : IGetVerbProcessor<GetArticleModel, GetArticleOptions>
	{

	}

	public class GetArticleProcessor : GetVerbProcessor<GetArticleModel, GetArticleOptions>, IGetArticleProcessor
	{
		private readonly IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> _configEntityLogger;
		private readonly IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration> _configEntityWriter;
		private readonly ILogger<GetArticleProcessor> _logger;

		public GetArticleProcessor(IGetEntityRepository<GetArticleModel> entityRepository,
			IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> entityLogger,
			IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration> entityWriter,
			ILogger<GetArticleProcessor> logger,
			IVerbositySetter verbositySetter)
			: base(entityRepository, entityLogger, entityWriter, logger, verbositySetter)
		{
			
			_configEntityLogger = entityLogger;
			_configEntityWriter = entityWriter;
			_logger = logger;

			logger.LogTrace("Created " + this.GetType().Name);
		}

		public override void ConfigureProcessor(GetArticleOptions options)
		{
			var config = options.GetArticleInclusionConfiguration();
			_configEntityLogger.Configure(config);
			_configEntityWriter.Configure(config);

			_logger.LogTrace("Configured processor with name " + this.GetType().Name);
		}
	}
}
