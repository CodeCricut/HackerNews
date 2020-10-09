using HackerNews.Domain;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Parameters;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels;
using HackerNews.ViewModels.Comments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{
	public class CommentsController : Controller
	{
		private static readonly string COMMENT_ENDPOINT = "comments";
		private readonly IApiReader<GetCommentModel> _commentReader;
		private readonly IApiModifier<Comment, PostCommentModel, GetCommentModel> _commentModifier;

		public CommentsController(
			IApiReader<GetCommentModel> commentReader,
			IApiModifier<Comment, PostCommentModel, GetCommentModel> commentModifier)
		{
			_commentReader = commentReader;
			_commentModifier = commentModifier;
		}

		public async Task<ViewResult> List(int pageNumber = 1, int pageSize = 10)
		{
			var pagingParams = new PagingParams { PageNumber = pageNumber, PageSize = pageSize };
			var commentModels = await _commentReader.GetEndpointAsync(COMMENT_ENDPOINT, pagingParams);

			var viewModel = new CommentsListViewModel { GetModels = commentModels };

			return View(viewModel);
		}

		public async Task<ViewResult> Details(int id)
		{
			var commentModel = await _commentReader.GetEndpointAsync(COMMENT_ENDPOINT, id);

			var model = new CommentDetailsViewModel { GetModel = commentModel };

			return View(model);
		}

		public ViewResult Create()
		{
			var model = new CommentCreateViewModel { PostModel = new PostCommentModel() };
			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> Post(PostCommentModel comment)
		{
			GetCommentModel getCommentModel = await _commentModifier.PostEndpointAsync(COMMENT_ENDPOINT, comment);

			return RedirectToAction("Details", new { getCommentModel.Id });
		}
	}
}
