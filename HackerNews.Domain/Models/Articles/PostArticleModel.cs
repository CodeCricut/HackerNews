using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Models.Articles
{
	public class PostArticleModel : PostEntityModel
	{
		[Required]
		public string Type { get; set; }
		[Required]
		public string Text { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		[Range(1, int.MaxValue)]
		public int BoardId { get; set; }

		// needed for model binding
		public PostArticleModel()
		{

		}
	}
}
