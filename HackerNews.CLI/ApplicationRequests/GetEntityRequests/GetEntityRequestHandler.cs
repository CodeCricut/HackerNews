using HackerNews.Domain.Common;
using MediatR;
using System.Threading.Tasks;

namespace HackerNews.CLI.ApplicationRequests.GetEntityRequests
{
	public interface IGetEntityRequestHandler<TGetModel, TInclusionConfiguration>
		where TGetModel : GetModelDto
	{
		Task HandleAsync(IGetEntityRequest<TGetModel, TInclusionConfiguration> request);
	}

	public class GetEntityRequestHandler<TGetModel, TInclusionConfiguration> : 
		IGetEntityRequestHandler<TGetModel, TInclusionConfiguration>
		where TGetModel : GetModelDto
	{
		private readonly IMediator _mediator;

		public GetEntityRequestHandler(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task HandleAsync(IGetEntityRequest<TGetModel, TInclusionConfiguration> request)
		{
			await _mediator.Send(request.CreateVerbosityCommand());

			TGetModel entity = await _mediator.Send(request.CreateGetEntityQuery());

			await _mediator.Send(request.CreateLogEntityCommand(entity));

			await _mediator.Send(request.CreateWriteEntityCommand(entity));
		}
	}
}
