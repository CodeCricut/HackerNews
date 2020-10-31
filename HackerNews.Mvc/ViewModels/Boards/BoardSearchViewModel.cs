using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Boards
{
	public class BoardSearchViewModel
	{
		public string SearchTerm { get; set; }
		public FrontendPage<GetBoardModel> BoardPage { get; set; }
	}
}
