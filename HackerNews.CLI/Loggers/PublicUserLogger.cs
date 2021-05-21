﻿using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Loggers
{
	public class PublicUserLogger : IEntityLogger<GetPublicUserModel>
	{
		private readonly ILogger<PublicUserLogger> _logger;
		private IEntityReader<GetPublicUserModel> _userReader;
		private PublicUserInclusionConfiguration _inclusionConfig;

		public PublicUserLogger(ILogger<PublicUserLogger> logger,
			IEntityReader<GetPublicUserModel> userReader)
		{
			_logger = logger;
			_userReader = userReader;
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
			Dictionary<string, string> userDict = _userReader.ReadAllKeyValues(user);

			_logger.LogInformation("---------------------");
			foreach (var kvp in userDict)
				_logger.LogInformation($"\t{kvp.Key}={kvp.Value}");
			_logger.LogInformation("---------------------");
		}
	}
}
