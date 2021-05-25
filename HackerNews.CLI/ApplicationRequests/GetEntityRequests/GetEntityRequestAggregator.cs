using HackerNews.Domain.Common;
using MediatR;
using System.Threading.Tasks;

namespace HackerNews.CLI.ApplicationRequests.GetEntityRequests
{
	public interface IGetEntityRequestAggregator<TGetModel>
		where TGetModel : GetModelDto
	{
		Task HandleAsync(IGetEntityRequest<TGetModel> request);
	}

	public class GetEntityRequestAggregator<TGetModel> : IGetEntityRequestAggregator<TGetModel>
		where TGetModel : GetModelDto
	{
		private readonly IMediator _mediator;

		public GetEntityRequestAggregator(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task HandleAsync(IGetEntityRequest<TGetModel> request)
		{
			await _mediator.Send(request.CreateVerbosityCommand());

			TGetModel entity = await _mediator.Send(request.CreateGetEntityQuery());

			await _mediator.Send(request.CreateLogEntityCommand(entity));

			await _mediator.Send(request.CreateWriteEntityCommand(entity));
		}
	}
}
