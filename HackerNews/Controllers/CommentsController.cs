using CleanEntityArchitecture.Domain;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels;
using HackerNews.ViewModels.Comments;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{
	public class CommentsController : Controller
	{
		private static readonly string COMMENT_ENDPOINT = "comments";
		private readonly IApiReader _apiReader;
		private readonly IApiModifier<Comment, PostCommentModel, GetCommentModel> _commentModifier;

		public CommentsController(
			IApiReader apiReader,
			IApiModifier<Comment, PostCommentModel, GetCommentModel> commentModifier)
		{
			_apiReader = apiReader;
			_commentModifier = commentModifier;
		}

		public async Task<ViewResult> List(int pageNumber = 1, int pageSize = 10)
		{
			var pagingParams = new PagingParams { PageNumber = pageNumber, PageSize = pageSize };
			var commentModels = await _apiReader.GetEndpointAsync<GetCommentModel>(COMMENT_ENDPOINT, pagingParams);

			var viewModel = new CommentsListViewModel { GetModels = commentModels.Items };

			return View(viewModel);
		}

		public async Task<ViewResult> Details(int id)
		{
			var commentModel = await _apiReader.GetEndpointAsync<GetCommentModel>(COMMENT_ENDPOINT, id);
			var board = await _apiReader.GetEndpointAsync<GetBoardModel>("Boards", commentModel.BoardId);
			var parentArticle = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", commentModel.ParentArticleId);
			var childComments = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", commentModel.CommentIds);
			var parentComment = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", commentModel.ParentCommentId);
			var user = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", commentModel.UserId);

			var model = new CommentDetailsViewModel { 
				GetModel = commentModel,
				Board = board,
				ParentArticle = parentArticle,
				PostCommentModel = new PostCommentModel(),
				ChildComments = childComments,
				ParentComment = parentComment,
				User = user
			};

			return View(model);
		}

		public ViewResult Create()
		{
			var model = new CommentCreateViewModel { PostModel = new PostCommentModel() };
			return View(model);
		}

		//[HttpPost]
		//public async Task<ActionResult> Post(PostCommentModel comment)
		//{
		//	GetCommentModel getCommentModel = await _commentModifier.PostEndpointAsync(COMMENT_ENDPOINT, comment);

		//	return RedirectToAction("Details", new { getCommentModel.Id });
		//}

		[HttpPost]
		public async Task<ActionResult> AddComment([Bind("GetModel, PostCommentModel")] CommentDetailsViewModel viewModel)
		{
			var commentAdded = viewModel.PostCommentModel;
			commentAdded.BoardId = viewModel.GetModel.BoardId;
			commentAdded.ParentCommentId = viewModel.GetModel.Id;

			await _commentModifier.PostEndpointAsync("Comments", commentAdded);

			return RedirectToAction("Details", new { id = viewModel.GetModel.Id });
		}
	}
}
