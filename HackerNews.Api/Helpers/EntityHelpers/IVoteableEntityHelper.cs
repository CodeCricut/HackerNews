using HackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityHelpers
{
	public interface IVoteableEntityHelper<EntityT> where EntityT : DomainEntity
	{
		Task VoteEntityAsync(int id, bool upvote);
	}
}
