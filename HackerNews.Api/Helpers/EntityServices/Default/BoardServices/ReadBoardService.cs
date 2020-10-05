using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models.Board;
using HackerNews.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.BoardServices
{
	public class ReadBoardService : ReadEntityService<Board, GetBoardModel>
	{
		public ReadBoardService(IMapper mapper, IEntityRepository<Board> entityRepository) : base(mapper, entityRepository)
		{
		}
	}
}
