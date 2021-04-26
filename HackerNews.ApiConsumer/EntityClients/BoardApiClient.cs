using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using System.Collections.Generic;
using System.Threading.Tasks;

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
