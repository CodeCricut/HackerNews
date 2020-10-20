using CleanEntityArchitecture.Controllers;
using CleanEntityArchitecture.Domain;
using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Api.Controllers.Base;
using HackerNews.Domain;
using HackerNews.Domain.Models.Comments;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
