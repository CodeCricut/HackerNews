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
		private readonly IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration> _configEntityWriter;

		public GetArticleProcessor(IGetEntityRepository<GetArticleModel> entityRepository,
			IEntityLogger<GetArticleModel> entityLogger,
			IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration> entityWriter)
			: base(entityRepository, entityLogger, entityWriter)
		{
			_configEntityWriter = entityWriter;
		}

		public override void ConfigureProcessor(GetArticleOptions options)
		{
			_configEntityWriter.Configure(options.GetArticleInclusionConfiguration());
		}
	}
}
