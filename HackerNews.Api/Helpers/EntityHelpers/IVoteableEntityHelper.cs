using HackerNews.Domain;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public interface IVoteableEntityHelper<EntityT> where EntityT : DomainEntity
	{
		Task VoteEntityAsync(int id, bool upvote, User currentUser);
	}
}
