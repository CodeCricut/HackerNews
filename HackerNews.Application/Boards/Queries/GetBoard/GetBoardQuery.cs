using HackerNews.Application.Common.Models.Boards;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
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
		public GetBoardHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<GetBoardModel> Handle(GetBoardQuery request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var board = await UnitOfWork.Boards.GetEntityAsync(request.Id);
				if (board == null) throw new NotFoundException();

				return Mapper.Map<GetBoardModel>(board);
			}
		}
	}
}
