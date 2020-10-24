using HackerNews.Domain;
using HackerNews.Domain.Models.Board;

namespace HackerNews.ViewModels.Boards
{
	public class BoardSearchViewModel
	{
		public string SearchTerm { get; set; }
		public Page<GetBoardModel> BoardPage { get; set; }
	}
}
