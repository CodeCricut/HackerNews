using CleanEntityArchitecture.Controllers;
using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Domain;
using HackerNews.Domain.Models.Comments;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.CommentControllers
{
	[Route("api/Comments")]
	public class WriteCommentController : WriteController<Comment, PostCommentModel, GetCommentModel>
	{
		public WriteCommentController(IWriteEntityService<Comment, PostCommentModel> writeService) : base(writeService)
		{
		}
	}
}
