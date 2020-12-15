using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerNews.Application.Common.DeletedEntityValidators
{
	public class DeletedUserPolicyValidator : IDeletedEntityPolicyValidator<User>
	{
		private readonly ICurrentUserService _currentUserService;

		public DeletedUserPolicyValidator(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}

		public User ValidateEntity(User entity, DeletedEntityPolicy policy)
		{
			if (entity.Deleted)
			{
				switch (policy)
				{
					case DeletedEntityPolicy.INACCESSIBLE:
						throw new EntityDeletedException();
					case DeletedEntityPolicy.OWNER:
						if (entity.Id != _currentUserService.UserId)
							throw new EntityDeletedException();
						break;
					case DeletedEntityPolicy.RESTRICTED:
						entity.Username = null;
						entity.FirstName = null;
						entity.LastName = null;
						entity.ProfileImage = null;
						break;
					case DeletedEntityPolicy.PUBLIC:
					default:
						break;
				}
			}
			return entity;
		}

		public IQueryable<User> ValidateEntityQuerable(IQueryable<User> entities, DeletedEntityPolicy policy)
		{
			Func<User, User> policySelector = entity =>
			{
				if (entity.Deleted)
				{
					switch (policy)
					{
						case DeletedEntityPolicy.INACCESSIBLE:
							return null;
						case DeletedEntityPolicy.OWNER:
							if (entity.Id != _currentUserService.UserId)
								return null;
							break;
						case DeletedEntityPolicy.RESTRICTED:
							entity.Username = null;
							entity.FirstName = null;
							entity.LastName = null;
							entity.ProfileImage = null;
							break;
						case DeletedEntityPolicy.PUBLIC:
						default:
							break;
					}
				}

				return entity;
			};

			// TODO: not effiecient to use where and convert to queryable
			IQueryable<User> validatedEntities = entities.Select(policySelector).Where(entity => entity != null).AsQueryable();
			return validatedEntities;
		}
	}
}
