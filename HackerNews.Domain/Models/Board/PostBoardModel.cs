using CleanEntityArchitecture.Domain;
using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Models.Board
{
	public class PostBoardModel : PostModelDto
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
	}
}
