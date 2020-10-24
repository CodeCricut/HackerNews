using HackerNews.Domain.Models.Board;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiBoardSubscriber
	{
		Task<GetBoardModel> AddSubscriber(int boardId);
	}
}
