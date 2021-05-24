using HackerNews.CLI.MediatR.Commands.LogEntity;
using HackerNews.CLI.MediatR.Commands.PostEntity;
using HackerNews.CLI.MediatR.Commands.PrintEntity;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.MediatR.Commands.SignIn;
using HackerNews.CLI.MediatR.Commands.WriteEntity;
using HackerNews.CLI.Options;
using HackerNews.CLI.Requests.PostBoard;
using MediatR;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.HostedServices
{
	public class PostBoardHostedService : IHostedService
	{
		private readonly PostBoardOptions _options;
		private readonly IMediator _mediator;

		public PostBoardHostedService(PostBoardOptions options,
			IMediator mediator)
		{
			_options = options;
			_mediator = mediator;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await _mediator.Send(new SetVerbosityCommand(_options));

			bool signInSuccessful = await _mediator.Send(new SignInCommand(_options));
			if (!signInSuccessful) return;

			var postedBoard = await _mediator.Send(new PostBoardCommand(_options));

			await _mediator.Send(new LogBoardCommand(postedBoard, _options));
			await _mediator.Send(new WriteBoardCommand(postedBoard, _options));
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
