using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Boards.Queries.GetBoardByTitle
{
	public class GetBoardByTitleQuery : IRequest<GetBoardModel>
	{
		public GetBoardByTitleQuery(string title)
		{
			Title = title;
		}

		public string Title { get; }
	}

	public class GetBoardByTitleHandler : DatabaseRequestHandler<GetBoardByTitleQuery, GetBoardModel>
	{
		public GetBoardByTitleHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetBoardModel> Handle(GetBoardByTitleQuery request, CancellationToken cancellationToken)
		{
			var boards = await UnitOfWork.Boards.GetEntitiesAsync();
			var boardByTitle = boards.FirstOrDefault(board => board.Title == request.Title);

			if (boardByTitle == null) throw new NotFoundException();

			return Mapper.Map<GetBoardModel>(boardByTitle);
		}
	}
}
