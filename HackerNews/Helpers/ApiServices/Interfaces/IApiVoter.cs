using CleanEntityArchitecture.Domain;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiVoter<TEntity>
		where TEntity : DomainEntity
	{
		Task VoteEntityAsync(int entityId, bool upvote);
	}
}
