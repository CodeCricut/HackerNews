using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Users;

namespace HackerNews.CLI.Verbs.Get
{
	public interface IGetPublicUserProcessor : IGetVerbProcessor<GetPublicUserModel>
	{

	}

	public class GetPublicUserProcessor : GetVerbProcessor<GetPublicUserModel>,
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

		public override void ConfigureProcessor(GetVerbOptions options)
		{
			_configEntityWriter.Configure(options.GetPublicUserInclusionConfiguration());
		}
	}
}
