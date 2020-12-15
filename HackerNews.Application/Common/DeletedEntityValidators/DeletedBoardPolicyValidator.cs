using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Application.Common.DeletedEntityValidators
{
	public class DeletedBoardPolicyValidator : IDeletedEntityPolicyValidator<Board>
	{
		private readonly ICurrentUserService _currentUserService;

		public DeletedBoardPolicyValidator(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}

		public Board ValidateEntity(Board entity, DeletedEntityPolicy policy)
		{
			if (entity.Deleted)
			{
				switch (policy)
				{
					case DeletedEntityPolicy.INACCESSIBLE:
						throw new EntityDeletedException();
					case DeletedEntityPolicy.OWNER:
						if (!entity.Moderators.Any(m => m.UserId == _currentUserService.UserId))
							throw new EntityDeletedException();
						break;
					case DeletedEntityPolicy.RESTRICTED:
						entity.Creator = null;
						entity.Moderators = new List<Domain.Entities.JoinEntities.BoardUserModerator>();
						entity.Subscribers = new List<Domain.Entities.JoinEntities.BoardUserSubscriber>();
						break;
					case DeletedEntityPolicy.PUBLIC:
					default:
						break;
				}
			}
			return entity;
		}

		public IQueryable<Board> ValidateEntityQuerable(IQueryable<Board> entities, DeletedEntityPolicy policy)
		{
			Func<Board, Board> policySelector = entity =>
			{
				if (entity.Deleted)
				{
					switch (policy)
					{
						case DeletedEntityPolicy.INACCESSIBLE:
							return null;
						case DeletedEntityPolicy.OWNER:
							if (!entity.Moderators.Any(m => m.UserId == _currentUserService.UserId))
								return null;
							break;
						case DeletedEntityPolicy.RESTRICTED:
							entity.Creator = null;
							entity.Moderators = new List<Domain.Entities.JoinEntities.BoardUserModerator>();
							entity.Subscribers = new List<Domain.Entities.JoinEntities.BoardUserSubscriber>();
							break;
						case DeletedEntityPolicy.PUBLIC:
						default:
							break;
					}
				}

				return entity;
			};

			// TODO: not effiecient to use where and convert to queryable
			IQueryable<Board> validatedEntities = entities.Select(policySelector).Where(entity => entity != null).AsQueryable();
			return validatedEntities;
		}
	}
}
