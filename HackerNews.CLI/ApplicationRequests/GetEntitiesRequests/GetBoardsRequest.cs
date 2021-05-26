using HackerNews.CLI.Application.MediatR.Commands.LogEntityPageWithConfiguration;
using HackerNews.CLI.Application.MediatR.Commands.WriteEntityPageWithConfiguration;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.MediatR.Queries.GetEntitiesByIds;
using HackerNews.CLI.Options;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.ApplicationRequests.GetEntitiesRequests
{
	public class GetBoardsRequest : IGetEntitiesRequest<GetBoardModel, BoardInclusionConfiguration>
	{
		public CreateVerbosityCommand CreateVerbosityCommand { get; }

		public CreateGetEntitiesByIdsQuery<GetBoardModel> CreateGetEntitiesByIdsQuery { get; }

		public CreateLogEntityPageCommand<GetBoardModel, BoardInclusionConfiguration> CreateLogEntityPageCommand { get; }

		public CreateWriteEntityPageCommand<GetBoardModel, BoardInclusionConfiguration> CreateWriteEntityPageCommand { get; }

		public GetBoardsRequest(GetBoardsOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateGetEntitiesByIdsQuery = () => new GetBoardsByIdsQuery(opts, opts);
			CreateLogEntityPageCommand = boardPage => new LogBoardPageCommand(boardPage, opts, opts.ToInclusionConfiguration());
			CreateWriteEntityPageCommand = boardPage => new WriteBoardPageCommand(boardPage, opts, opts.ToInclusionConfiguration());
		}
	}
}
