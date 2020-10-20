using CleanEntityArchitecture.Domain;
using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Models.Comments
{
	public class PostCommentModel : PostModelDto
	{
		[Required]
		public string Text { get; set; }
		[Range(0, int.MaxValue)]
		public int ParentCommentId { get; set; }
		[Range(0, int.MaxValue)]
		public int ParentArticleId { get; set; }

		public PostCommentModel()
		{

		}
	}
}
