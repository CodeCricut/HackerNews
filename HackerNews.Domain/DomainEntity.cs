using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain
{
	public abstract class DomainEntity
	{
		[Key]
		public int Id { get; set; }
		public bool Deleted { get; set; }
	}
}
