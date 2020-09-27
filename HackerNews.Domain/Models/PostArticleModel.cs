using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Models
{
	public class PostArticleModel : PostEntityModel
	{
		[Required]
		public string Type { get; set; }
		[Required]
		public int AuthorId { get; set; }
		[Required]
		public string Text { get; set; }
		[Required]
		public string Title { get; set; }

		// needed for model binding
		public PostArticleModel()
		{

		}
	}
}
