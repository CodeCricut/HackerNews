using HackerNews.Domain.Models.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiBoardModeratorAdder
	{
		Task<GetBoardModel> AddModerator(int boardId, int moderatorId); 
	}
}
