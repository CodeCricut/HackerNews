using HackerNews.Domain.Models.Board;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Interfaces
{
	public interface IBoardUserManagementService
	{
		Task<GetBoardModel> AddBoardSubscriberAsync(int boardId);
		Task<GetBoardModel> AddBoardModeratorAsync(int boardId, int moderatorId);
	}
}
