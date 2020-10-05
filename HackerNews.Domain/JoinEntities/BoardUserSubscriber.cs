namespace HackerNews.Domain.JoinEntities
{
	public class BoardUserSubscriber
	{
		public int BoardId { get; set; }
		public Board Board { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
