using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Boards;
using Microsoft.Extensions.Logging;

namespace HackerNews.ApiConsumer.EntityClients
{
	public interface IBoardApiClient : IEntityApiClient<PostBoardModel, GetBoardModel> { }

	internal class BoardApiClient : EntityApiClient<PostBoardModel, GetBoardModel>, IBoardApiClient
	{
		public BoardApiClient(IApiClient apiClient, ILogger<BoardApiClient> logger) : base(apiClient, logger, "boards")
		{
		}
	}
}
