using HackerNews.Domain;
using HackerNews.Domain.Models.Board;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Interfaces
{
	public interface IBoardUserManagementService
	{
		Task<GetBoardModel> AddBoardSubscriberAsync(int boardId, User currentUser);
		Task<GetBoardModel> AddBoardModeratorAsync(int boardId, User currentUser, int moderatorId);
	}
}
