using HackerNews.Domain.Common.Helpers.Validation;
using HackerNews.Domain.Common.Models.Boards;
using System.ComponentModel.DataAnnotations;

namespace HackerNews.Mvc.ViewModels.Boards
{
	public class BoardCreateModel
	{
		[Required, ValidateObject]
		public PostBoardModel Board { get; set; }
	}
}
