using HackerNews.CLI.InclusionConfiguration;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Loggers
{
	public class ConfigurablePublicUserLogger : ConfigurableEntityLogger<GetPublicUserModel, PublicUserInclusionConfiguration>
	{
		public ConfigurablePublicUserLogger(ILogger<ConfigurableEntityLogger<GetPublicUserModel, PublicUserInclusionConfiguration>> logger, IEntityInclusionReader<PublicUserInclusionConfiguration, GetPublicUserModel> articleInclusionReader, PublicUserInclusionConfiguration inclusionConfig) : base(logger, articleInclusionReader, inclusionConfig)
		{
		}

		protected override string GetEntityName()
			=> "User";

		protected override string GetEntityNamePlural()
			=> "Users";
	}
}
