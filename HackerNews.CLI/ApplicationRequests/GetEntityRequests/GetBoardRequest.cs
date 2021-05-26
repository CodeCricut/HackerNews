using HackerNews.CLI.Application.MediatR.Commands.LogEntityWithConfiguration;
using HackerNews.CLI.Application.MediatR.Commands.WriteEntityWithConfiguration;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.MediatR.Queries.GetEntityById;
using HackerNews.CLI.Options;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.ApplicationRequests.GetEntityRequests
{
	public class GetBoardRequest : IGetEntityRequest<GetBoardModel, BoardInclusionConfiguration>
	{
		public CreateVerbosityCommand CreateVerbosityCommand { get; }

		public CreateGetEntityQuery<GetBoardModel> CreateGetEntityQuery { get; }

		public CreateLogEntityCommand<GetBoardModel, BoardInclusionConfiguration> CreateLogEntityCommand { get; }

		public CreateWriteEntityCommand<GetBoardModel, BoardInclusionConfiguration> CreateWriteEntityCommand { get; }

		public GetBoardRequest(GetBoardByIdOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateGetEntityQuery = () => new GetBoardByIdQuery(opts);
			CreateLogEntityCommand = board => new LogBoardCommand(board, opts, opts.ToInclusionConfiguration());
			CreateWriteEntityCommand = board => new WriteBoardCommand(board, opts, opts.ToInclusionConfiguration());
		}
	}
}
