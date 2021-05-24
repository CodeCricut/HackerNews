using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Requests.GetCommentById
{
	public class GetCommentByIdRequestFactory
	{
		private readonly ILogger<GetCommentByIdRequest> _logger;
		private readonly IVerbositySetter _verbositySetter;
		private readonly IConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration> _entityLogger;
		private readonly IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration> _entityWriter;
		private readonly IEntityFinder<GetCommentModel> _getEntityRepo;

		public GetCommentByIdRequestFactory(
			ILogger<GetCommentByIdRequest> logger,
			IVerbositySetter verbositySetter,
			IConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration> entityLogger,
			IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration> entityWriter,
			IEntityFinder<GetCommentModel> getEntityRepo
			)
		{
			_logger = logger;
			_verbositySetter = verbositySetter;
			_entityLogger = entityLogger;
			_entityWriter = entityWriter;
			_getEntityRepo = getEntityRepo;
		}

		public GetCommentByIdRequest Create(
			CommentInclusionConfiguration commentInclusionConfiguration,
			bool verbose,
			bool print,
			string fileLocation,
			int id)
		{
			return new GetCommentByIdRequest(
				_logger,
				_verbositySetter,
				_entityLogger,
				_entityWriter,
				_getEntityRepo,
				commentInclusionConfiguration,
				verbose,
				print,
				fileLocation,
				id);
		}
	}
}
