using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Models.Comments
{
	public class PostCommentModel : PostEntityModel
	{
		[Required]
		public string Text { get; set; }
		public int ParentCommentId { get; set; }
		public int ParentArticleId { get; set; }

		public PostCommentModel()
		{

		}
	}
}
