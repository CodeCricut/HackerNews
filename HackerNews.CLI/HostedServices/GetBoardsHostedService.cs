using HackerNews.CLI.MediatR.Commands.LogEntityPage;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.MediatR.Commands.WriteBoardPage;
using HackerNews.CLI.MediatR.Queries.GetEntitiesByIds;
using HackerNews.CLI.Options;
using HackerNews.CLI.Requests.GetBoards;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class GetBoardsHostedService : IHostedService
	{
		private readonly GetBoardsOptions _options;
		private readonly IMediator _mediator;

		public GetBoardsHostedService(
			GetBoardsOptions options,
			IMediator mediator
			)
		{
			_options = options;
			_mediator = mediator;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await _mediator.Send(new SetVerbosityCommand(_options));

			PaginatedList<GetBoardModel> boardPage = await _mediator.Send(new GetBoardsByIdsQuery(_options, _options));

			await _mediator.Send(new LogBoardPageWithConfigurationCommand(boardPage, _options, _options));

			await _mediator.Send(new WriteBoardPageWithConfigurationCommand(boardPage, _options, _options));

		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}