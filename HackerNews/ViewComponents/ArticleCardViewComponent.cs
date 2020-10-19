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
		private readonly IApiReader _apiReader;

		public ArticleCardViewComponent(IApiReader apiReader)
		{
			_apiReader = apiReader;
		}

		public async Task<IViewComponentResult> InvokeAsync(GetArticleModel articleModel)
		{
			var board = await _apiReader.GetEndpointAsync<GetBoardModel>("Boards", articleModel.BoardId);
			var user = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", articleModel.UserId);

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
