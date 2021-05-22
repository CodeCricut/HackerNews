using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Requests.GetArticleById
{
	public class GetArticleByIdRequestFactory
	{
		private readonly ILogger<GetArticleByIdRequest> _logger;
		private readonly IVerbositySetter _verbositySetter;
		private readonly IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> _entityLogger;
		private readonly IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration> _entityWriter;
		private readonly IGetEntityRepository<GetArticleModel> _getArticleRepo;

		public GetArticleByIdRequestFactory(
			ILogger<GetArticleByIdRequest> logger,
			IVerbositySetter verbositySetter,
			IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> entityLogger,
			IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration> entityWriter,
			IGetEntityRepository<GetArticleModel> getArticleRepo)
		{
			_logger = logger;
			_verbositySetter = verbositySetter;
			_entityLogger = entityLogger;
			_entityWriter = entityWriter;
			_getArticleRepo = getArticleRepo;
		}

		public GetArticleByIdRequest Create(
			ArticleInclusionConfiguration articleInclusionConfiguration,
			bool verbosity,
			bool print,
			string fileLocation,
			int id)
		{
			return new GetArticleByIdRequest(
				_logger,
				_verbositySetter,
				_entityLogger,
				_entityWriter,
				_getArticleRepo,
				articleInclusionConfiguration,
				verbosity,
				print,
				fileLocation,
				id);
		}
	}
}
