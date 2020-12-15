namespace HackerNews.Domain.Common
{
	/// <summary>
	/// The base type for all entities stored in the database.
	/// </summary>
	public abstract class DomainEntity
	{
		public int Id { get; set; }
		public bool Deleted { get; set; }
	}
}
