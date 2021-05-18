using HackerNews.ApiConsumer.Core;
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
		public GetPublicUserProcessor(IEntityApiClient<RegisterUserModel, GetPublicUserModel> entityApiClient, IEntityLogger<GetPublicUserModel> entityLogger)
			: base(entityApiClient, entityLogger)
		{
		}
	}
}
