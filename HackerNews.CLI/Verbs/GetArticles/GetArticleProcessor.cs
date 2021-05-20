using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Articles;

namespace HackerNews.CLI.Verbs.GetArticles
{
	public interface IGetArticleProcessor : IGetVerbProcessor<GetArticleModel, GetArticleOptions>
	{

	}

	public class GetArticleProcessor : GetVerbProcessor<GetArticleModel, GetArticleOptions>, IGetArticleProcessor
	{
		private readonly IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> _configEntityLogger;
		private readonly IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration> _configEntityWriter;

		public GetArticleProcessor(IGetEntityRepository<GetArticleModel> entityRepository,
			IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> entityLogger,
			IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration> entityWriter)
			: base(entityRepository, entityLogger, entityWriter)
		{
			_configEntityLogger = entityLogger;
			_configEntityWriter = entityWriter;
		}

		public override void ConfigureProcessor(GetArticleOptions options)
		{
			var config = options.GetArticleInclusionConfiguration();
			_configEntityLogger.Configure(config);
			_configEntityWriter.Configure(config);
		}
	}
}
