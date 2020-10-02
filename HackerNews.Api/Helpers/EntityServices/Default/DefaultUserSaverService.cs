using AutoMapper;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Default
{
	public class DefaultUserSaverService : UserSaverService
	{
		public DefaultUserSaverService(IEntityRepository<Article> articleRepo, IEntityRepository<Comment> commentRepo, IMapper mapper) : base(articleRepo, commentRepo, mapper)
		{
		}
	}
}
