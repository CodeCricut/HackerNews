using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Api.Controllers.Base;
using HackerNews.Domain;
using HackerNews.Domain.Models.Board;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.BoardControllers
{
	[Route("api/Boards")]
	public class ReadBoardController : ReadEntityController<Board, GetBoardModel>
	{
		public ReadBoardController(IReadEntityService<Board, GetBoardModel> readService) : base(readService)
		{
		}
	}
}
