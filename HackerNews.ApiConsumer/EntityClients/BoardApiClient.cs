using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.ApiConsumer.EntityClients
{
	public interface IBoardApiClient : IEntityApiClient<PostBoardModel, GetBoardModel> { }

	internal class BoardApiClient : EntityApiClient<PostBoardModel, GetBoardModel>, IBoardApiClient
	{
		public BoardApiClient(IApiClient apiClient) : base(apiClient, "boards")
		{
		}
	}
}
