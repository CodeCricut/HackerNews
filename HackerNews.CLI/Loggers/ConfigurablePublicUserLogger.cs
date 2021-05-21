using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace HackerNews.CLI.Loggers
{
	public class ConfigurablePublicUserLogger : IConfigurableEntityLogger<GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		private readonly ILogger<ConfigurablePublicUserLogger> _logger;
		private IEntityInclusionReader<PublicUserInclusionConfiguration, GetPublicUserModel> _userInclusionReader;
		private PublicUserInclusionConfiguration _inclusionConfig;

		public ConfigurablePublicUserLogger(ILogger<ConfigurablePublicUserLogger> logger,
			IEntityInclusionReader<PublicUserInclusionConfiguration, GetPublicUserModel> userInclusionReader)
		{
			_logger = logger;
			_userInclusionReader = userInclusionReader;
			_inclusionConfig = new PublicUserInclusionConfiguration();
		}

		public void Configure(PublicUserInclusionConfiguration config)
		{
			_inclusionConfig = config;
		}

		public void LogEntity(GetPublicUserModel user)
		{
			LogPublicUser(user);
		}

		public void LogEntityPage(PaginatedList<GetPublicUserModel> userPage)
		{
			_logger.LogInformation($"USER PAGE {userPage.PageIndex}/{userPage.TotalPages}; Showing {userPage.PageSize} / {userPage.TotalCount} Users" +
				$"");
			foreach (var user in userPage.Items)
				LogPublicUser(user);
		}

		private void LogPublicUser(GetPublicUserModel user)
		{
			Dictionary<string, string> userDict = _userInclusionReader.ReadIncludedKeyValues(_inclusionConfig, user);

			_logger.LogInformation("---------------------");
			foreach (var kvp in userDict)
				_logger.LogInformation($"\t{kvp.Key}={kvp.Value}");
			_logger.LogInformation("---------------------");
		}
	}
}
