using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Models
{
	public class PostCommentModel : PostEntityModel
	{
		[Required]
		public string AuthorName { get; set; }
		[Required]
		public string Text { get; set; }
		public int ParentCommentId { get; set; }
		public int ParentArticleId { get; set; }
	}
}
