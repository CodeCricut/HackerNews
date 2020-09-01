using System.Collections.Generic;

namespace HackerNews.Domain
{
	public class Comment : DomainEntity
	{
		public int Id { get; set; }
		public bool Deleted { get; set; }
		public string AuthorName { get; set; }
		public string Text { get; set; }
		public string  Url { get; set; }
		public int Karma { get; set; }
		public List<Comment> Comments { get; set; }
		public Comment? ParentComment { get; set; }
		public Article? ParentArticle { get; set; }

		public Comment()
		{
			Comments = new List<Comment>();
		}

		public Comment(Comment commentToClone)
		{
			Id = commentToClone.Id;
			Deleted = commentToClone.Deleted;
			AuthorName = commentToClone.AuthorName;
			Text = commentToClone.Text;
			Url = commentToClone.Url;
			Karma = commentToClone.Karma;
			Comments = commentToClone.Comments;
			ParentComment = commentToClone.ParentComment;
			ParentArticle = commentToClone.ParentArticle;
		}
	}
}