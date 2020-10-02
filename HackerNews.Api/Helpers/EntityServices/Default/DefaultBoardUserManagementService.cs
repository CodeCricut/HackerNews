using AutoMapper;
using HackerNews.Api.Helpers.EntityServices.Base;
using HackerNews.Domain;
using HackerNews.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Default
{
	public class DefaultBoardUserManagementService : BoardUserManagmentService
	{
		public DefaultBoardUserManagementService(IEntityRepository<Board> boardRepo, IEntityRepository<User> userRepo, IMapper mapper) : base(boardRepo, userRepo, mapper)
		{
		}
	}
}
