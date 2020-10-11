using HackerNews.Domain.Models.Board;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiBoardModeratorAdder
	{
		Task<GetBoardModel> AddModerator(int boardId, int moderatorId);
	}
}
