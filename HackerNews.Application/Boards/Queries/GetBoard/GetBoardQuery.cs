using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Boards.Queries.GetBoard
{
	public class GetBoardQuery : IRequest<GetBoardModel>
	{
		public GetBoardQuery(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}

	public class GetBoardHandler : DatabaseRequestHandler<GetBoardQuery, GetBoardModel>
	{
		public GetBoardHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetBoardModel> Handle(GetBoardQuery request, CancellationToken cancellationToken)
		{
			var board = await UnitOfWork.Boards.GetEntityAsync(request.Id);
			if (board == null) throw new NotFoundException();

			return Mapper.Map<GetBoardModel>(board);
		}
	}
}
