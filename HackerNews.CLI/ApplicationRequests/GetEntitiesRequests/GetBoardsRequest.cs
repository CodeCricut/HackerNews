using HackerNews.CLI.MediatR.Commands.LogEntityPage;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.MediatR.Commands.WriteBoardPage;
using HackerNews.CLI.MediatR.Queries.GetEntitiesByIds;
using HackerNews.CLI.Options;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.ApplicationRequests.GetEntitiesRequests
{
	public class GetBoardsRequest : IGetEntitiesRequest<GetBoardModel>
	{
		public CreateVerbosityCommand CreateVerbosityCommand { get; }

		public CreateGetEntitiesByIdsQuery<GetBoardModel> CreateGetEntitiesByIdsQuery { get; }

		public CreateLogEntityPageCommand<GetBoardModel> CreateLogEntityPageCommand { get; }

		public CreateWriteEntityPageCommand<GetBoardModel> CreateWriteEntityPageCommand { get; }

		public GetBoardsRequest(GetBoardsOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateGetEntitiesByIdsQuery = () => new GetBoardsByIdsQuery(opts, opts);
			CreateLogEntityPageCommand = boardPage => new LogBoardPageWithConfigurationCommand(boardPage, opts, opts);
			CreateWriteEntityPageCommand = boardPage => new WriteBoardPageWithConfigurationCommand(boardPage, opts, opts);
		}
	}
}
