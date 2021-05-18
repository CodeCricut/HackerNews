using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Loggers;
using HackerNews.Domain.Common.Models.Users;

namespace HackerNews.CLI.Verbs.Get
{
	public interface IGetPublicUserProcessor : IGetVerbProcessor<RegisterUserModel, GetPublicUserModel>
	{

	}

	public class GetPublicUserProcessor : GetVerbProcessor<RegisterUserModel, GetPublicUserModel>,
		IGetPublicUserProcessor
	{
		public GetPublicUserProcessor(IEntityApiClient<RegisterUserModel, GetPublicUserModel> entityApiClient, IEntityLogger<GetPublicUserModel> entityLogger, IEntityWriter<GetPublicUserModel> entityWriter) : base(entityApiClient, entityLogger, entityWriter)
		{
		}

		protected override void ConfigureWriter(GetVerbOptions options, IEntityWriter<GetPublicUserModel> writer)
		{
			throw new System.NotImplementedException();
		}
	}
}
