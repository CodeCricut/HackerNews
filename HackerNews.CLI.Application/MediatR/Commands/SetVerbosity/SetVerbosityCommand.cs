using HackerNews.CLI.Configuration;
using HackerNews.CLI.Verbs.GetEntity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.SetVerbosity
{
	public class SetVerbosityCommand : IRequest
	{

		public SetVerbosityCommand(IVerbosityOptions options)
		{
			Options = options;
		}

		public IVerbosityOptions Options { get; }
	}

	public class SetVerbosityCommandHandler : IRequestHandler<SetVerbosityCommand>
	{
		private readonly IVerbositySetter _verbositySetter;

		public SetVerbosityCommandHandler(IVerbositySetter verbositySetter)
		{
			_verbositySetter = verbositySetter;
		}

		public Task<Unit> Handle(SetVerbosityCommand request, CancellationToken cancellationToken)
		{
			_verbositySetter.SetVerbository(request.Options.Verbose);
			return Unit.Task;
		}
	}
}
