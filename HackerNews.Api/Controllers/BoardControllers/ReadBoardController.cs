using CleanEntityArchitecture.Controllers;
using CleanEntityArchitecture.EntityModelServices;
using HackerNews.Domain;
using HackerNews.Domain.Models.Board;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers.BoardControllers
{
	[Route("api/Boards")]
	public class ReadBoardController : ReadController<Board, GetBoardModel>
	{
		public ReadBoardController(IReadEntityService<Board, GetBoardModel> readService) : base(readService)
		{
		}
	}
}
