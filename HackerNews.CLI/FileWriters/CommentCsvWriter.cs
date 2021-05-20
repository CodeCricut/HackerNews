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
		private readonly IEntityInclusionReader<CommentInclusionConfiguration, GetCommentModel> _commentInclusionReader;
		private CommentInclusionConfiguration _inclusionConfig;

		public CommentCsvWriter(IFileWriter writer, 
			ILogger<CommentCsvWriter> logger,
			IEntityInclusionReader<CommentInclusionConfiguration, GetCommentModel> commentInclusionReader)
		{
			_fileWriter = writer;
			_logger = logger;
			_commentInclusionReader = commentInclusionReader;
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
			var keys = _commentInclusionReader.ReadIncludedKeys(_inclusionConfig);

			StringBuilder sb = new StringBuilder();
			foreach (var key in keys)
			{
				sb.Append($"{key},");
			}

			return sb.ToString();
		}

		private string GetBodyLine(GetCommentModel comment)
		{
			var values = _commentInclusionReader.ReadIncludedValues(_inclusionConfig, comment);

			StringBuilder sb = new StringBuilder();
			foreach (var value in values)
			{
				sb.Append($"{value},");
			}

			return sb.ToString();
		}
	}
}
