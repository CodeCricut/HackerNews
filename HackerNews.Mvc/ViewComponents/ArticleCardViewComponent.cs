using HackerNews.Application.Boards.Queries.GetBoard;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Users.Queries.GetPublicUser;
using HackerNews.Domain.Exceptions;
using HackerNews.Mvc.ViewModels.ViewComponents.ArticleCard;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewComponents
{
	public class ArticleCardViewComponent : ViewComponent
	{
		private readonly IMediator _mediator;

		public ArticleCardViewComponent(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IViewComponentResult> InvokeAsync(GetArticleModel articleModel)
		{
			GetBoardModel board;
			GetPublicUserModel user;
			try
			{
				board = await _mediator.Send(new GetBoardQuery(articleModel.BoardId));
			}
			catch (NotFoundException ex)
			{
				board = new GetBoardModel();
			}

			try
			{
				user = await _mediator.Send(new GetPublicUserQuery(articleModel.UserId));
			}
			catch (NotFoundException ex)
			{
				user = new GetPublicUserModel();
			}

			var viewModel = new ArticleCardViewModel
			{
				Article = articleModel,
				Board = board,
				User = user
			};
			return View(viewModel);
		}
	}
}
