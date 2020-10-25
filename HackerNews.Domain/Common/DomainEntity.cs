namespace HackerNews.Domain.Common
{
	public abstract class DomainEntity
	{
		public int Id { get; set; }
		public bool Deleted { get; set; }
	}
}
