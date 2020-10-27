using HackerNews.Application.Common.Models.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Mvc.ViewModels.Boards
{
	public class BoardCreateModel
	{
		public PostBoardModel Board { get; set; }
	}
}
