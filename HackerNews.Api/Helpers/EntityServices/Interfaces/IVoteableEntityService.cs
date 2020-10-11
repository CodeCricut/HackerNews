using CleanEntityArchitecture.Domain;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public interface IVoteableEntityService<EntityT> where EntityT : DomainEntity
	{
		Task VoteEntityAsync(int id, bool upvote);
	}
}
