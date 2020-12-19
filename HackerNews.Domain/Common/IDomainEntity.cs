namespace HackerNews.Domain.Common
{
	/// <summary>
	/// The base type for all entities stored in the database.
	/// </summary>
	public interface IDomainEntity
	{
		int Id { get; set; }
		bool Deleted { get; set; }
	}
}
