namespace HackerNews.Domain.Entities.JoinEntities
{
	public class UserCommentDislikes
	{
		public int UserId { get; set; }
		public User User { get; set; }
		public int CommentId { get; set; }
		public Comment Comment { get; set; }
	}
}
