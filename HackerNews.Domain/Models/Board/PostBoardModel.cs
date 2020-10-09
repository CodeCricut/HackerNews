using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Models.Board
{
	public class PostBoardModel : PostEntityModel
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
	}
}
