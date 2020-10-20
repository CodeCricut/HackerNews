using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
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
	public class CommentCardViewComponent : ViewComponent
	{
		private readonly IApiReader _apiReader;

		public CommentCardViewComponent(IApiReader apiReader)
		{
			_apiReader = apiReader;
		}

		public async Task<IViewComponentResult> InvokeAsync(GetCommentModel commentModel)
		{
			var board = await _apiReader.GetEndpointAsync<GetBoardModel>("boards", commentModel.BoardId);
			var parentArticle = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", commentModel.ParentArticleId);
			var parentComment = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", commentModel.ParentCommentId);
			var user = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", commentModel.UserId);

			var viewModel = new CommentCardViewModel
			{
				Comment = commentModel,
				Board = board,
				ParentArticle = parentArticle,
				ParentComment = parentComment,
				User = user
			};

			return View(viewModel);
		}
	}
}
