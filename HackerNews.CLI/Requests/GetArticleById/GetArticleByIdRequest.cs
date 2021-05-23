using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Request.Core;
using HackerNews.Domain.Common.Models.Articles;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Requests.GetArticleById
{
	public class GetArticleByIdRequest : IRequest
	{
		private readonly ILogger<GetArticleByIdRequest> _logger;
		private readonly IVerbositySetter _verbositySetter;
		private readonly IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> _entityLogger;
		private readonly IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration> _entityWriter;
		private readonly IGetEntityRepository<GetArticleModel> _getArticleRepo;
		private readonly ArticleInclusionConfiguration _articleInclusionConfiguration;
		private readonly bool _verbose;
		private readonly bool _print;
		private readonly string _fileLocation;
		private readonly int _id;

		public GetArticleByIdRequest(
			ILogger<GetArticleByIdRequest> logger,
			IVerbositySetter verbositySetter,
			IConfigurableEntityLogger<GetArticleModel, ArticleInclusionConfiguration> entityLogger,
			IConfigurableEntityWriter<GetArticleModel, ArticleInclusionConfiguration> entityWriter,
			IGetEntityRepository<GetArticleModel> getArticleRepo,

			ArticleInclusionConfiguration articleInclusionConfiguration,
			bool verbose,
			bool print,
			string fileLocation,
			int id)
		{
			_logger = logger;
			_verbositySetter = verbositySetter;
			_entityLogger = entityLogger;
			_entityWriter = entityWriter;
			_getArticleRepo = getArticleRepo;
			_articleInclusionConfiguration = articleInclusionConfiguration;
			_verbose = verbose;
			_print = print;
			_fileLocation = fileLocation;
			_id = id;
		}

		public async Task ExecuteAsync()
		{
			_verbositySetter.SetVerbository(_verbose);

			GetArticleModel article = await _getArticleRepo.GetByIdAsync(_id);
			if (article == null)
			{
				_logger.LogWarning($"Could not find a article with the ID of {_id}. Aborting request...");
				return;
			}

			if (_print)
			{
				_entityLogger.Configure(_articleInclusionConfiguration);
				_entityLogger.LogEntity(article);
			}

			if (!string.IsNullOrEmpty(_fileLocation))
			{
				_entityWriter.Configure(_articleInclusionConfiguration);
				await _entityWriter.WriteEntityAsync(_fileLocation, article);
			}
		}

		public Task CancelAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
