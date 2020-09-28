using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Models.Comments;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class CommentsController : ODataController
	{
		private readonly IEntityHelper<Comment, PostCommentModel, GetCommentModel> _commentHelper;
		private readonly IVoteableEntityHelper<Comment> _commentVoter;

		public CommentsController(IEntityHelper<Comment, PostCommentModel, GetCommentModel> commentHelper,
			IVoteableEntityHelper<Comment> commentVoter)
		{
			_commentHelper = commentHelper;
			_commentVoter = commentVoter;
		}

		#region Create
		[HttpPost]
		public async Task<IActionResult> PostCommentAsync([FromBody] PostCommentModel commentModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var addedModel = await _commentHelper.PostEntityModelAsync(commentModel);

			return Ok(addedModel);
		}

		[HttpPost("range")]
		public async Task<IActionResult> PostCommentsAsync([FromBody] List<PostCommentModel> commentModels)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			await _commentHelper.PostEntityModelsAsync(commentModels);

			return Ok();
		}
		#endregion

		#region Read
		[EnableQuery]
		public async Task<IActionResult> GetCommentAsync(int key)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var commentModel = await _commentHelper.GetEntityModelAsync(key);

			return Ok(commentModel);
		}

		[EnableQuery]
		public async Task<IActionResult> GetCommentsAsync()
		{
			var commentModels = await _commentHelper.GetAllEntityModelsAsync();

			return Ok(commentModels);
		}
		#endregion

		#region Update
		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutCommentAsync(int id, [FromBody] PostCommentModel commentModel)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			var updatedModel = await _commentHelper.PutEntityModelAsync(id, commentModel);

			return Ok(updatedModel);
		}

		[HttpPost("vote/{commentId:int}")]
		public async Task<IActionResult> VoteCommentAsync(int commentId, [FromBody] bool upvote)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			await _commentVoter.VoteEntityAsync(commentId, upvote);

			return Ok();
		}
		#endregion

		#region Delete
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteCommentAsync(int id)
		{
			if (!ModelState.IsValid) throw new InvalidPostException(ModelState);

			await _commentHelper.SoftDeleteEntityAsync(id);

			return Ok();
		}
		#endregion
	}
}
