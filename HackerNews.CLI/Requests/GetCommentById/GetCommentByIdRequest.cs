using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Requests.GetCommentById
{
	public class GetCommentByIdRequest : IRequest
	{
		private readonly ILogger<GetCommentByIdRequest> _logger;
		private readonly IVerbositySetter _verbositySetter;
		private readonly IConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration> _entityLogger;
		private readonly IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration> _entityWriter;
		private readonly IGetEntityRepository<GetCommentModel> _getCommentModel;
		private readonly CommentInclusionConfiguration _commentInclusiononfiguration;
		private readonly bool _verbose;
		private readonly bool _print;
		private readonly string _fileLocation;
		private readonly int _id;

		public GetCommentByIdRequest(
			ILogger<GetCommentByIdRequest> logger,
			IVerbositySetter verbositySetter,
			IConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration> entityLogger,
			IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration> entityWriter,
			IGetEntityRepository<GetCommentModel> getCommentModel,

			CommentInclusionConfiguration commentInclusiononfiguration,
			bool verbose,
			bool print,
			string fileLocation,
			int id)
		{
			_logger = logger;
			_verbositySetter = verbositySetter;
			_entityLogger = entityLogger;
			_entityWriter = entityWriter;
			_getCommentModel = getCommentModel;
			_commentInclusiononfiguration = commentInclusiononfiguration;
			_verbose = verbose;
			_print = print;
			_fileLocation = fileLocation;
			_id = id;
		}

		public async Task ExecuteAsync()
		{
			_verbositySetter.SetVerbository(_verbose);

			GetCommentModel comment = await _getCommentModel.GetByIdAsync(_id);
			if (comment == null)
			{
				_logger.LogWarning($"Could not find a comment with the ID of {_id}. Aborting request...");
				return;
			}

			if (_print)
			{
				_entityLogger.Configure(_commentInclusiononfiguration);
				_entityLogger.LogEntity(comment);
			}

			if (!string.IsNullOrEmpty(_fileLocation))
			{
				_entityWriter.Configure(_commentInclusiononfiguration);
				await _entityWriter.WriteEntityAsync(_fileLocation, comment);
			}
		}

		public Task CancelAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
