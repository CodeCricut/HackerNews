using HackerNews.Application.Common.Models.Boards;
using HackerNews.Mvc.Models;

namespace HackerNews.Mvc.ViewModels.Home
{
	public class HomeBoardsViewModel
	{
		public FrontendPage<GetBoardModel> BoardPage { get; set; }
	}
}
