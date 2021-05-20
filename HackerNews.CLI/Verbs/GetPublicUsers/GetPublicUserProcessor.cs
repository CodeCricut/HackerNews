using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.Logging;

namespace HackerNews.CLI.Verbs.GetPublicUsers
{
	public interface IGetPublicUserProcessor : IGetVerbProcessor<GetPublicUserModel, GetPublicUsersOptions>
	{

	}

	public class GetPublicUserProcessor : GetVerbProcessor<GetPublicUserModel, GetPublicUsersOptions>,
		IGetPublicUserProcessor
	{
		private readonly IConfigurableEntityLogger<GetPublicUserModel, PublicUserInclusionConfiguration> _configEntityLogger;
		private readonly IConfigurableEntityWriter<GetPublicUserModel, PublicUserInclusionConfiguration> _configEntityWriter;

		public GetPublicUserProcessor(IGetEntityRepository<GetPublicUserModel> entityRepository,
			IConfigurableEntityLogger<GetPublicUserModel, PublicUserInclusionConfiguration> entityLogger,
			IConfigurableEntityWriter<GetPublicUserModel, PublicUserInclusionConfiguration> entityWriter,
			ILogger<GetPublicUserProcessor> logger)
			: base(entityRepository, entityLogger, entityWriter, logger)
		{
			_configEntityLogger = entityLogger;
			_configEntityWriter = entityWriter;
		}

		public override void ConfigureProcessor(GetPublicUsersOptions options)
		{
			var config = options.GetPublicUserInclusionConfiguration();
			_configEntityLogger.Configure(config);
			_configEntityWriter.Configure(config);
		}
	}
}
