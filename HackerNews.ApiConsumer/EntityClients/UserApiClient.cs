using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Users;

namespace HackerNews.ApiConsumer.EntityClients
{
	// TODO: violates interface segregation principal but #YOLO
	public interface IUserApiClient : IEntityApiClient<RegisterUserModel, GetPublicUserModel>
	{

	}

	internal class UserApiClient : EntityApiClient<RegisterUserModel, GetPublicUserModel>, IUserApiClient
	{
		public UserApiClient(IApiClient apiClient) : base(apiClient, "users")
		{
		}
	}
}
