using HackerNews.CLI.MediatR.Commands.PrintEntity;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.MediatR.Commands.WriteEntity;
using HackerNews.CLI.MediatR.Queries.GetEntitiesByIds;
using HackerNews.CLI.MediatR.Queries.GetEntityById;
using HackerNews.CLI.Options;
using HackerNews.CLI.Requests.EntityRequest;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class GetBoardByIdHostedService : IHostedService
	{
		private readonly GetBoardByIdOptions _options;
		private readonly IMediator _mediator;

		//private GetBoardByIdRequest _request;

		public GetBoardByIdHostedService(
			GetBoardByIdOptions options,
			IMediator mediator
			)
		{
			_options = options;
			_mediator = mediator;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await _mediator.Send(new SetVerbosityCommand(_options));
			
			GetBoardModel board = await _mediator.Send(new GetBoardByIdQuery(_options));
			
			await _mediator.Send(new LogBoardWithConfigurationCommand(board, _options, _options));
			
			await _mediator.Send(new WriteBoardWithConfigurationCommand(board, _options, _options));

		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
