﻿using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using MediatR;
using System.Threading.Tasks;

namespace HackerNews.CLI.ApplicationRequests.GetEntitiesRequests
{
	public interface IGetEntitiesRequestHandler<TGetModel>
		where TGetModel : GetModelDto
	{
		Task HandleAsync(IGetEntitiesRequest<TGetModel> request);
	}

	public class GetEntitiesRequestHandler<TGetModel> : IGetEntitiesRequestHandler<TGetModel>
		where TGetModel : GetModelDto
	{
		private readonly IMediator _mediator;

		public GetEntitiesRequestHandler(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task HandleAsync(IGetEntitiesRequest<TGetModel> request)
		{
			await _mediator.Send(request.CreateVerbosityCommand());

			PaginatedList<TGetModel> entityPage = await _mediator.Send(request.CreateGetEntitiesByIdsQuery());

			await _mediator.Send(request.CreateLogEntityPageCommand(entityPage));

			await _mediator.Send(request.CreateWriteEntityPageCommand(entityPage));
		}
	}
}