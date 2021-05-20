using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Users;

namespace HackerNews.CLI.Verbs.GetPublicUsers
{
	public interface IGetPublicUserProcessor : IGetVerbProcessor<GetPublicUserModel, GetPublicUsersOptions>
	{

	}

	public class GetPublicUserProcessor : GetVerbProcessor<GetPublicUserModel, GetPublicUsersOptions>,
		IGetPublicUserProcessor
	{
		private readonly IConfigurableEntityWriter<GetPublicUserModel, PublicUserInclusionConfiguration> _configEntityWriter;

		public GetPublicUserProcessor(IGetEntityRepository<GetPublicUserModel> entityRepository,
			IEntityLogger<GetPublicUserModel> entityLogger,
			IConfigurableEntityWriter<GetPublicUserModel, PublicUserInclusionConfiguration> entityWriter)
			: base(entityRepository, entityLogger, entityWriter)
		{
			_configEntityWriter = entityWriter;
		}

		public override void ConfigureProcessor(GetPublicUsersOptions options)
		{
			_configEntityWriter.Configure(options.GetPublicUserInclusionConfiguration());
		}
	}
}
