using AutoMapper;
using CleanEntityArchitecture.EntityModelServices;
using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using HackerNews.Domain.Models.Board;

namespace HackerNews.Api.Helpers.EntityServices.Base.BoardServices
{
	public class ReadBoardService : ReadEntityService<Board, GetBoardModel>
	{
		public ReadBoardService(IMapper mapper, IReadEntityRepository<Board> readRepository) : base(mapper, readRepository)
		{
		}
	}
}
