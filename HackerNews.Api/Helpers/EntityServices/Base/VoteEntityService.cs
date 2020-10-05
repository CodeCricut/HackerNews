using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base
{
	public abstract class VoteEntityService<TEntity> : IVoteableEntityService<TEntity>
		where TEntity : DomainEntity
	{
		public abstract Task VoteEntityAsync(int id, bool upvote, Domain.User currentUser);
	}
}
