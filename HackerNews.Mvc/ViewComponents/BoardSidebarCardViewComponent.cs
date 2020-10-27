using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Application.Users.Queries.GetPublicUser;
using HackerNews.Application.Users.Queries.GetPublicUsersByIds;
using HackerNews.Domain.Exceptions;
using HackerNews.Mvc.Models;
using HackerNews.Mvc.ViewModels.ViewComponents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewComponents
{
	public class BoardSidebarCardViewComponent : ViewComponent
	{
		private readonly IMediator _mediator;

		public BoardSidebarCardViewComponent(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IViewComponentResult> InvokeAsync(GetBoardModel boardModel)
		{
			var pagingParams = new PagingParams() { PageNumber = 1, PageSize = 5 };
			var moderators = await _mediator.Send(new GetPublicUsersByIdsQuery(boardModel.ModeratorIds, pagingParams));

			bool loggedIn = false;
			bool moderating = false;
			bool subscribed = false;

			try
			{
				var user = await _mediator.Send(new GetAuthenticatedUserQuery());
				
				loggedIn = user != null && user.Id > 0;

				moderating = loggedIn
				? user.BoardsModerating.Contains(boardModel.Id)
				: false;

				subscribed = loggedIn
					? user.BoardsSubscribed.Contains(boardModel.Id)
					: false;
			}
			catch (NotFoundException ex) { }

			var model = new BoardSidebarViewModel 
			{
				Board = boardModel,
				ModeratorPage = new FrontendPage<GetPublicUserModel>(moderators),
				LoggedIn = loggedIn,
				Moderating = moderating,
				Subscribed = subscribed
			};
			return View(model);
		}
	}
}
