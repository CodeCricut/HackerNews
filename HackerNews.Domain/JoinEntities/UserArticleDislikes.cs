namespace HackerNews.Domain
{
	public class UserArticleDislikes
	{
		public int UserId { get; set; }
		public User User { get; set; }
		public int ArticleId { get; set; }
		public Article Article { get; set; }
	}
}
