using CleanEntityArchitecture.Controllers;
using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Api.Controllers.Base;
using HackerNews.Domain;
using HackerNews.Domain.Models.Board;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.BoardControllers
{
	[Route("api/Boards")]
	public class WriteBoardController : WriteController<Board, PostBoardModel, GetBoardModel>
	{
		public WriteBoardController(IWriteEntityService<Board, PostBoardModel> writeService) : base(writeService)
		{
		}
	}
}
