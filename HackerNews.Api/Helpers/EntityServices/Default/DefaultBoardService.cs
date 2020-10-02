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
	public class DefaultBoardService : BoardService
	{
		public DefaultBoardService(IEntityRepository<Board> entityRepository, IMapper mapper) : base(entityRepository, mapper)
		{
		}
	}
}
