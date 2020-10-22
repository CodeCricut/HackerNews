using CleanEntityArchitecture.Domain;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using HackerNews.Domain.Models.Board;
using HackerNews.Domain.Models.Comments;
using HackerNews.Domain.Models.Users;
using HackerNews.Helpers.ApiServices.Interfaces;
using HackerNews.ViewModels;
using HackerNews.ViewModels.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{
	public class CommentsController : Controller
	{
		private static readonly string COMMENT_ENDPOINT = "comments";
		private readonly IApiReader _apiReader;
		private readonly IApiModifier<Comment, PostCommentModel, GetCommentModel> _commentModifier;
		private readonly IApiVoter<Comment> _commentVoter;
		private readonly IApiUserSaver<Comment> _commentSaver;

		public CommentsController(
			IApiReader apiReader,
			IApiModifier<Comment, PostCommentModel, GetCommentModel> commentModifier,
			IApiVoter<Comment> commentVoter,
			IApiUserSaver<Comment> commentSaver)
		{
			_apiReader = apiReader;
			_commentModifier = commentModifier;
			_commentVoter = commentVoter;
			_commentSaver = commentSaver;
		}


		public async Task<ViewResult> Details(int id, PagingParams pagingParams)
		{
			var commentModel = await _apiReader.GetEndpointAsync<GetCommentModel>(COMMENT_ENDPOINT, id);
			var board = await _apiReader.GetEndpointAsync<GetBoardModel>("Boards", commentModel.BoardId);
			var parentArticle = await _apiReader.GetEndpointAsync<GetArticleModel>("articles", commentModel.ParentArticleId);
			var childComments = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", commentModel.CommentIds, pagingParams);
			var parentComment = await _apiReader.GetEndpointAsync<GetCommentModel>("comments", commentModel.ParentCommentId);
			var user = await _apiReader.GetEndpointAsync<GetPublicUserModel>("users", commentModel.UserId);

			var privateUser = await _apiReader.GetEndpointAsync<GetPrivateUserModel>("users/me");
			var loggedIn = privateUser != null && privateUser.Id != 0;

			var savedComment = loggedIn
				? privateUser.SavedComments.Contains(id)
				: false;


			var model = new CommentDetailsViewModel
			{
				Board = board,
				ChildCommentPage = new Page<GetCommentModel>(childComments),
				Comment = commentModel,
				LoggedIn = loggedIn,
				ParentArticle = parentArticle,
				ParentComment = parentComment,
				PostCommentModel = new PostCommentModel(),
				User = user,
				UserSavedComment = savedComment
			};

			return View(model);
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult> AddComment([Bind("GetModel, PostCommentModel")] CommentDetailsViewModel viewModel)
		{
			viewModel.PostCommentModel.ParentCommentId = viewModel.Comment.Id;

			await _commentModifier.PostEndpointAsync("Comments", viewModel.PostCommentModel);

			return RedirectToAction("Details", new { id = viewModel.Comment.Id });
		}

		[Authorize]
		public async Task<ActionResult> Vote(int id, bool upvote)
		{
			await _commentVoter.VoteEntityAsync(id, upvote);
			return RedirectToAction("Details", new { id });
		}

		[Authorize]
		public async Task<ActionResult> SaveComment(int id)
		{
			await _commentSaver.SaveEntityToUserAsync(id);

			return RedirectToAction("Details", new { id });
		}

		public async Task<ActionResult<CommentSearchViewModel>> Search(string searchTerm, PagingParams pagingParams)
		{
			var comments = await _apiReader.GetEndpointWithQueryAsync<GetCommentModel>(COMMENT_ENDPOINT, searchTerm, pagingParams);

			var model = new CommentSearchViewModel { SearchTerm = searchTerm, CommentPage = new Page<GetCommentModel>(comments) };
			return View(model);
		}
	}
}
