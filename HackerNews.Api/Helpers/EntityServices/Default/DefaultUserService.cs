using AutoMapper;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.EF.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Default
{
	public class DefaultUserService : UserService
	{
		public DefaultUserService(IEntityRepository<User> userRepository, IMapper mapper, IOptions<AppSettings> appSettings) : base(userRepository, mapper)
		{
		}
	}
}
