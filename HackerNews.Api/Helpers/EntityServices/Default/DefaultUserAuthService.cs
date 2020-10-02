using AutoMapper;
using HackerNews.Api.Helpers.EntityServices.Base;
using HackerNews.Domain;
using HackerNews.EF.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Default
{
	public class DefaultUserAuthService : UserAuthService
	{
		public DefaultUserAuthService(IMapper mapper, IOptions<AppSettings> appSettings, IEntityRepository<User> userRepository, IUserRepository userLoginRepo) 
			: base(mapper, appSettings, userRepository, userLoginRepo)
		{
		}
	}
}
