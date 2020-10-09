using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models.Board;
using HackerNews.EF.Repositories;

namespace HackerNews.Api.Helpers.EntityServices.Base.BoardServices
{
	public class ReadBoardService : ReadEntityService<Board, GetBoardModel>
	{
		public ReadBoardService(IMapper mapper, IEntityRepository<Board> entityRepository) : base(mapper, entityRepository)
		{
		}
	}
}
