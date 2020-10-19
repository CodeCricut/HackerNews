using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels.Other;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewComponents
{
	public class ArticleCardViewComponent : ViewComponent
	{
		private readonly IApiReader<GetBoardModel> _boardApiReader;
		private readonly IApiReader<GetPublicUserModel> _publicUserApiReader;

		public ArticleCardViewComponent(IApiReader<GetBoardModel> boardApiReader, IApiReader<GetPublicUserModel> publicUserApiReader)
		{
			_boardApiReader = boardApiReader;
			_publicUserApiReader = publicUserApiReader;
		}

		public async Task<IViewComponentResult> InvokeAsync(GetArticleModel articleModel)
		{
			var board = await _boardApiReader.GetEndpointAsync("Boards", articleModel.BoardId);
			var user = await _publicUserApiReader.GetEndpointAsync("users", articleModel.UserId);

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
