using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Services
{
	public class PublicUserLogger : IEntityLogger<GetPublicUserModel>
	{
		private readonly ILogger<PublicUserLogger> _logger;

		public PublicUserLogger(ILogger<PublicUserLogger> logger)
		{
			_logger = logger;
		}

		public void LogEntity(GetPublicUserModel user)
		{
			// TODO
			string printString = $"USER {user.Id}: Username={user.Username}";
			_logger.LogInformation(printString);
		}

		public void LogEntityPage(PaginatedList<GetPublicUserModel> userPage)
		{
			// TODO:
			_logger.LogInformation(userPage.PageSize.ToString());
		}
	}
}
