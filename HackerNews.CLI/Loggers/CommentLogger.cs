﻿using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Loggers
{
	public class CommentLogger : IEntityLogger<GetCommentModel>
	{
		private readonly ILogger<CommentLogger> _logger;
		private readonly IEntityReader<GetCommentModel> _commentReader;

		public CommentLogger(ILogger<CommentLogger> logger,
			IEntityReader<GetCommentModel> commentInclusionReader)
		{
			_logger = logger;
			_commentReader = commentInclusionReader;
		}

		public void LogEntity(GetCommentModel comment)
		{
			LogComment(comment);
		}

		public void LogEntityPage(PaginatedList<GetCommentModel> commentPage)
		{
			_logger.LogInformation($"COMMENT PAGE {commentPage.PageIndex}/{commentPage.TotalPages}; Showing {commentPage.PageSize} / {commentPage.TotalCount} Comments");
			foreach (var comment in commentPage.Items)
				LogComment(comment);
		}

		private void LogComment(GetCommentModel comment)
		{
			Dictionary<string, string> boardDict = _commentReader.ReadAllKeyValues(comment);

			_logger.LogInformation("---------------------");
			foreach (var kvp in boardDict)
				_logger.LogInformation($"\t{kvp.Key}={kvp.Value}");
			_logger.LogInformation("---------------------");
		}
	}
}
