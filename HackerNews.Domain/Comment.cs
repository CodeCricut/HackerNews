using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackerNews.Domain
{
	public class Comment : DomainEntity
	{
		public int UserId { get; set; }
		public User User { get; set; }
		public string Text { get; set; }
		public string Url { get; set; }
		public int Karma { get; set; }

		public List<UserComment> UsersSaved { get; set; }

		[ForeignKey("Comment")]
		public int? ParentCommentId { get; set; }
		public virtual Comment ParentComment { get; set; }


		// not sure exactly what it does, but it is necessary because the children are of the same type as the parent
		[InverseProperty("ParentComment")]
		public virtual List<Comment> ChildComments { get; set; }

		[ForeignKey("Article")]
		public virtual int? ParentArticleId { get; set; }
		public virtual Article ParentArticle { get; set; }

		public Comment()
		{
			ChildComments = new List<Comment>();
		}

		public Comment(Comment commentToClone)
		{
			Id = commentToClone.Id;
			Deleted = commentToClone.Deleted;
			User = commentToClone.User;
			Text = commentToClone.Text;
			Url = commentToClone.Url;
			Karma = commentToClone.Karma;
			ChildComments = commentToClone.ChildComments;
			ParentComment = commentToClone.ParentComment;
			ParentArticle = commentToClone.ParentArticle;
		}
	}
}