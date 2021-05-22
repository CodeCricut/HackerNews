using HackerNews.CLI.InclusionConfiguration;
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
		private readonly IEntityInclusionReader<PublicUserInclusionConfiguration, GetPublicUserModel> _userInclusionReader;
		private PublicUserInclusionConfiguration _inclusionConfig;

		public PublicUserCsvWriter(IFileWriter fileWriter,
			ILogger<PublicUserCsvWriter> logger,
			IEntityInclusionReader<PublicUserInclusionConfiguration, GetPublicUserModel> userInclusionReader)
		{
			_fileWriter = fileWriter;
			_logger = logger;
			_userInclusionReader = userInclusionReader;
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
			var keys = _userInclusionReader.ReadIncludedKeys(_inclusionConfig);

			StringBuilder sb = new StringBuilder();
			foreach (var key in keys)
			{
				sb.Append($"{key},");
			}

			return sb.ToString();
		}

		private string GetBodyLine(GetPublicUserModel user)
		{
			var values = _userInclusionReader.ReadIncludedValues(_inclusionConfig, user);

			StringBuilder sb = new StringBuilder();
			foreach (var value in values)
			{
				sb.Append($"{value},");
			}

			return sb.ToString();
		}
	}
}
