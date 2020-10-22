using CleanEntityArchitecture.Controllers;
using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Domain;
using HackerNews.Domain.Models.Comments;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.CommentControllers
{
	[Route("api/Comments")]
	public class ReadCommentController : ReadController<Comment, GetCommentModel>
	{
		public ReadCommentController(IReadEntityService<Comment, GetCommentModel> readService) : base(readService)
		{
		}
	}
}
