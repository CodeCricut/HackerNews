using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public class CommentCsvWriter : IConfigurableEntityWriter<GetCommentModel, CommentInclusionConfiguration>
	{
		private readonly IFileWriter _fileWriter;
		private readonly ILogger<CommentCsvWriter> _logger;
		private CommentInclusionConfiguration _inclusionConfig;

		public CommentCsvWriter(IFileWriter writer, ILogger<CommentCsvWriter> logger)
		{
			_fileWriter = writer;
			_logger = logger;
			_inclusionConfig = new CommentInclusionConfiguration();
		}

		public void Configure(CommentInclusionConfiguration config)
		{
			_inclusionConfig = config;
		}

		public Task WriteEntityAsync(string fileLoc, GetCommentModel entity)
		{
			List<GetCommentModel> comment = new List<GetCommentModel>
			{
				entity
			};
			return WriteEntitiesToFileAsync(fileLoc, comment);
		}

		public Task WriteEntityPageAsync(string fileLoc, PaginatedList<GetCommentModel> entityPage)
		{
			return WriteEntitiesToFileAsync(fileLoc, entityPage.Items);
		}

		private async Task WriteEntitiesToFileAsync(string fileLoc, IEnumerable<GetCommentModel> comments)
		{
			List<string> lines = new List<string>
			{
				GetHeadLine()
			};

			foreach (var comment in comments)
				lines.Add(GetBodyLine(comment));

			_logger.LogInformation($"Adding {lines.Count} lines to {fileLoc}...");

			try
			{
				await _fileWriter.WriteToFileAsync(fileLoc, lines);
				_logger.LogInformation($"Wrote {lines.Count} lines to {fileLoc}...");
			}
			catch (Exception)
			{
				_logger.LogError($"Error adding {lines.Count} lines to {fileLoc}. Aborting");
				throw;
			}
		}

		private string GetHeadLine()
		{
			StringBuilder head = new StringBuilder();

			if (_inclusionConfig.IncludeId)
				head.Append("ID,");
			if (_inclusionConfig.IncludeUserId)
				head.Append("USER ID,");
			if (_inclusionConfig.IncludeText)
				head.Append("TEXT,");
			if (_inclusionConfig.IncludeUrl)
				head.Append("URL");
			if (_inclusionConfig.IncludeKarma)
				head.Append("KARMA,");
			if (_inclusionConfig.IncludeCommentIds)
				head.Append("COMMENT IDS,");
			if (_inclusionConfig.IncludeParentCommentId)
				head.Append("PARENT COMMENT ID");
			if (_inclusionConfig.IncludeParentArticleId)
				head.Append("PARENT ARTICLE ID");
			if (_inclusionConfig.IncludeDeleted)
				head.Append("DELETED");
			if (_inclusionConfig.IncludeUsersLiked)
				head.Append("USERS LIKED");
			if (_inclusionConfig.IncludeUsersDisliked)
				head.Append("USERS DISLIKED");
			if (_inclusionConfig.IncludePostDate)
				head.Append("POST DATE");
			if (_inclusionConfig.IncludeBoardId)
				head.Append("BOARD ID");

			return head.ToString();
		}

		private string GetBodyLine(GetCommentModel comment)
		{
			char delimiter = ',';
			StringBuilder body = new StringBuilder();

			if (_inclusionConfig.IncludeId)
				body.Append($"{comment.Id},");
			if (_inclusionConfig.IncludeUserId)
				body.Append($"{comment.UserId},");
			if (_inclusionConfig.IncludeText)
				body.Append($"{comment.Text.Quote()},");
			if (_inclusionConfig.IncludeUrl)
				body.Append($"{comment.Url.Quote()},");
			if (_inclusionConfig.IncludeKarma)
				body.Append($"{comment.Karma},");
			if (_inclusionConfig.IncludeCommentIds)
				body.Append($"{comment.CommentIds.ToDelimitedList(delimiter).Quote()},");
			if (_inclusionConfig.IncludeParentCommentId)
				body.Append($"{comment.ParentCommentId},");
			if (_inclusionConfig.IncludeParentArticleId)
				body.Append($"{comment.ParentArticleId},");
			if (_inclusionConfig.IncludeDeleted)
				body.Append($"{comment.Deleted},");
			if (_inclusionConfig.IncludeUsersLiked)
				body.Append($"{comment.UsersLiked.ToDelimitedList(delimiter).Quote()},");
			if (_inclusionConfig.IncludeUsersDisliked)
				body.Append($"{comment.UsersDisliked.ToDelimitedList(delimiter).Quote()},");
			if (_inclusionConfig.IncludePostDate)
				body.Append($"{comment.PostDate.ToString().Quote()},");
			if (_inclusionConfig.IncludeBoardId)
				body.Append($"{comment.BoardId},");

			return body.ToString();
		}
	}
}
