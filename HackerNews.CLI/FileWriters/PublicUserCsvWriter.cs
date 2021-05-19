using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.FileWriters
{
	public class PublicUserCsvWriter : IConfigurableEntityWriter<GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		private readonly IFileWriter _fileWriter;
		private readonly ILogger<PublicUserCsvWriter> _logger;
		private PublicUserInclusionConfiguration _inclusionConfig;

		public PublicUserCsvWriter(IFileWriter fileWriter, ILogger<PublicUserCsvWriter> logger)
		{
			_fileWriter = fileWriter;
			_logger = logger;
			_inclusionConfig = new PublicUserInclusionConfiguration();
		}

		public void Configure(PublicUserInclusionConfiguration config)
		{
			_inclusionConfig = config;
		}

		public Task WriteEntityAsync(string fileLoc, GetPublicUserModel entity)
		{
			List<GetPublicUserModel> users = new List<GetPublicUserModel>()
			{
				entity
			};
			return WriteEntitiesToFileAsync(fileLoc, users);
		}

		public Task WriteEntityPageAsync(string fileLoc, PaginatedList<GetPublicUserModel> entityPage)
		{
			return WriteEntitiesToFileAsync(fileLoc, entityPage.Items);
		}

		private async Task WriteEntitiesToFileAsync(string fileLoc, IEnumerable<GetPublicUserModel> users)
		{
			List<string> lines = new List<string>
			{
				GetHeadLine()
			};

			foreach (var user in users)
				lines.Add(GetBodyLine(user));

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
			if (_inclusionConfig.IncludeUsername)
				head.Append("USERNAME,");
			if (_inclusionConfig.IncludeKarma)
				head.Append("KARMA,");
			if (_inclusionConfig.IncludeArticleIds)
				head.Append("ARTICLE IDS,");
			if (_inclusionConfig.IncludeCommentIds)
				head.Append("COMMENT IDS,");
			if (_inclusionConfig.IncludeJoinDate)
				head.Append("JOIN DATE,");
			if (_inclusionConfig.IncludeDeleted)
				head.Append("DELETED,");
			if (_inclusionConfig.IncludeProfileImageId)
				head.Append("PROFILE IMAGE ID,");

			return head.ToString();
		}

		private string GetBodyLine(GetPublicUserModel user)
		{
			char delimiter = ',';
			StringBuilder body = new StringBuilder();


			if (_inclusionConfig.IncludeId)
				body.Append($"{user.Id},");
			if(_inclusionConfig.IncludeUsername)
				body.Append($"{user.Username.Quote()},");
			if (_inclusionConfig.IncludeKarma)
				body.Append($"{user.Karma},");
			if (_inclusionConfig.IncludeArticleIds)
				body.Append($"{user.ArticleIds.ToDelimitedList(delimiter).Quote()},");
			if (_inclusionConfig.IncludeCommentIds)
				body.Append($"{user.CommentIds.ToDelimitedList(delimiter).Quote()},");
			if (_inclusionConfig.IncludeJoinDate)
				body.Append($"{user.JoinDate.ToString().Quote()},");
			if (_inclusionConfig.IncludeDeleted)
				body.Append($"{user.Deleted},");
			if (_inclusionConfig.IncludeProfileImageId)
				body.Append($"{user.ProfileImageId},");

			return body.ToString();
		}
	}
}
