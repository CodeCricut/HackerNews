using AutoMapper;
using HackerNews.Domain.Models.Users;
using HackerNews.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.User
{
	public class ReadPublicUserService : ReadEntityService<Domain.User, GetPublicUserModel>
	{
		public ReadPublicUserService(IMapper mapper, IEntityRepository<Domain.User> entityRepository) : base(mapper, entityRepository)
		{
		}
	}
}
