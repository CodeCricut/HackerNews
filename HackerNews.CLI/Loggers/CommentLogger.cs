using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace HackerNews.CLI.Loggers
{
	public class CommentLogger : IConfigurableEntityLogger<GetCommentModel, CommentInclusionConfiguration>
	{
		private readonly ILogger<CommentLogger> _logger;
		private readonly IEntityInclusionReader<CommentInclusionConfiguration, GetCommentModel> _commentInclusionReader;
		private CommentInclusionConfiguration _inclusionConfig;

		public CommentLogger(ILogger<CommentLogger> logger, 
			IEntityInclusionReader<CommentInclusionConfiguration, GetCommentModel> commentInclusionReader)
		{
			_logger = logger;
			_commentInclusionReader = commentInclusionReader;
			_inclusionConfig = new CommentInclusionConfiguration();
		}

		public void Configure(CommentInclusionConfiguration config)
		{
			_inclusionConfig = config;
		}

		public void LogEntity(GetCommentModel comment)
		{
			LogComment(comment);
		}

		public void LogEntityPage(PaginatedList<GetCommentModel> commentPage)
		{
			_logger.LogInformation($"COMMENT PAGE {commentPage.PageIndex}/{commentPage.TotalPages}; Showing {commentPage.PageSize} / {commentPage.TotalCount} Boards");
			foreach (var comment in commentPage.Items)
				LogComment(comment);
		}

		private void LogComment(GetCommentModel comment)
		{
			Dictionary<string, string> boardDict = _commentInclusionReader.ReadIncludedKeyValues(_inclusionConfig, comment);

			_logger.LogInformation("---------------------");
			foreach (var kvp in boardDict)
				_logger.LogInformation($"\t{kvp.Key}={kvp.Value}");
			_logger.LogInformation("---------------------");
		}
	}
}
