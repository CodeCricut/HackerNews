using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Api.Controllers.Base;
using HackerNews.Domain;
using HackerNews.Domain.Models.Comments;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.CommentControllers
{
	[Route("api/Comments")]
	public class ReadCommentController : ReadEntityController<Comment, GetCommentModel>
	{
		public ReadCommentController(IReadEntityService<Comment, GetCommentModel> readService) : base(readService)
		{
		}
	}
}
