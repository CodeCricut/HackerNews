using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Loggers
{
	public class PublicUserLogger : EntityLogger<GetPublicUserModel>, IEntityLogger<GetPublicUserModel>
	{
		public PublicUserLogger(ILogger<EntityLogger<GetPublicUserModel>> logger, IEntityReader<GetPublicUserModel> entityReader) : base(logger, entityReader)
		{
		}

		protected override string GetEntityName()
			=> "User";

		protected override string GetEntityPlural()
			=> "Users";
	}
}
