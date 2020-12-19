using HackerNews.Domain.Common;

namespace HackerNews.Domain.Entities
{
	public class Image : IDomainEntity
	{
		public int Id { get; set; }
		public bool Deleted { get; set; }

		public string ImageTitle { get; set; }
		public string ImageDescription { get; set; }
		public byte[] ImageData { get; set; }
		public string ContentType { get; set; }

		public int? ArticleId { get; set; }
		public Article Article { get; set; }
		public int? BoardId { get; set; }
		public Board Board { get; set; }
		public int? UserId { get; set; }
		public User User { get; set; }
	}
}
