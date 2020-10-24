using HackerNews.Domain;
using HackerNews.Domain.Models.Board;

namespace HackerNews.ViewModels.Home
{
	public class HomeBoardsViewModel
	{
		public Page<GetBoardModel> BoardPage { get; set; }
	}
}
