using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using MediatR;
using System.Threading.Tasks;

namespace HackerNews.CLI.ApplicationRequests.GetEntitiesRequests
{
	public interface IGetEntitiesRequestHandler<TGetModel, TInclusionConfiguration>
		where TGetModel : GetModelDto
	{
		Task HandleAsync(IGetEntitiesRequest<TGetModel, TInclusionConfiguration> request);
	}

	public class GetEntitiesRequestHandler<TGetModel, TInclusionConfiguration> : 
		IGetEntitiesRequestHandler<TGetModel, TInclusionConfiguration>
		where TGetModel : GetModelDto
	{
		private readonly IMediator _mediator;

		public GetEntitiesRequestHandler(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task HandleAsync(IGetEntitiesRequest<TGetModel, TInclusionConfiguration> request)
		{
			await _mediator.Send(request.CreateVerbosityCommand());

			PaginatedList<TGetModel> entityPage = await _mediator.Send(request.CreateGetEntitiesByIdsQuery());

			await _mediator.Send(request.CreateLogEntityPageCommand(entityPage));

			await _mediator.Send(request.CreateWriteEntityPageCommand(entityPage));
		}
	}
}
