using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using System;
using System.Linq;

namespace HackerNews.Application.Common.DeletedEntityValidators
{


	public class DeletedArticlePolicyValidator : IDeletedEntityPolicyValidator<Article>
	{
		private readonly ICurrentUserService _currentUserService;

		public DeletedArticlePolicyValidator(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}

		public Article ValidateEntity(Article entity, DeletedEntityPolicy policy)
		{
			if (entity.Deleted)
			{
				switch (policy)
				{
					case DeletedEntityPolicy.INACCESSIBLE:
						throw new EntityDeletedException();
					case DeletedEntityPolicy.OWNER:
						if (entity.UserId != _currentUserService.UserId)
							throw new EntityDeletedException();
						break;
					case DeletedEntityPolicy.RESTRICTED:
						entity.User = null;
						entity.UserId = 0;
						break;
					case DeletedEntityPolicy.PUBLIC:
					default:
						break;
				}
			}
			return entity;
		}

		public IQueryable<Article> ValidateEntityQuerable(IQueryable<Article> entities, DeletedEntityPolicy policy)
		{
			Func<Article, Article> policySelector = entity =>
			{
				if (entity.Deleted)
				{
					switch (policy)
					{
						case DeletedEntityPolicy.INACCESSIBLE:
							return null;
						case DeletedEntityPolicy.OWNER:
							if (entity.UserId != _currentUserService.UserId)
								return null;
							break;
						case DeletedEntityPolicy.RESTRICTED:
							entity.User = null;
							entity.UserId = 0;
							break;
						case DeletedEntityPolicy.PUBLIC:
						default:
							break;
					}
				}

				return entity;
			};

			// TODO: not effiecient to use where and convert to queryable
			IQueryable<Article> validatedEntities = entities.Select(policySelector).Where(entity => entity != null).AsQueryable();
			return validatedEntities;
		}
	}
}