using CleanEntityArchitecture.Domain;
using HackerNews.Domain;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels.Other;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.ViewComponents
{
	public class BoardSidebarCardViewComponent : ViewComponent
	{
		private readonly IApiReader _apiReader;

		public BoardSidebarCardViewComponent(IApiReader apiReader)
		{
			_apiReader = apiReader;
		}

		public async Task<IViewComponentResult> InvokeAsync(GetBoardModel boardModel)
		{
			var pagingParams = new PagingParams() { PageNumber = 1, PageSize = 5 };
			var moderators = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", boardModel.ModeratorIds, pagingParams);

			var user = await _apiReader.GetEndpointAsync<GetPrivateUserModel>("users/me");
			bool loggedIn = user != null && user.Id > 0;

			var moderating = loggedIn
				? user.BoardsModerating.Contains(boardModel.Id)
				: false;

			var subscribed = loggedIn
				? user.BoardsSubscribed.Contains(boardModel.Id)
				: false;

			var viewModel = new BoardSidebarCardViewModel
			{
				Board = boardModel,
				ModeratorPage = new Page<GetPublicUserModel>(moderators),
				LoggedIn = loggedIn,
				Moderating = moderating,
				Subscribed = subscribed
			};

			return View(viewModel);
		}
	}
}
