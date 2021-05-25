using HackerNews.CLI.MediatR.Commands.LogEntity;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.MediatR.Commands.WriteEntity;
using HackerNews.CLI.MediatR.Queries.GetEntityById;
using HackerNews.CLI.Options;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.ApplicationRequests.GetEntityRequests
{
	public class GetBoardRequest : IGetEntityRequest<GetBoardModel>
	{
		public CreateVerbosityCommand CreateVerbosityCommand { get; }

		public CreateGetEntityQuery<GetBoardModel> CreateGetEntityQuery { get; }

		public CreateLogEntityCommand<GetBoardModel> CreateLogEntityCommand { get; }

		public CreateWriteEntityCommand<GetBoardModel> CreateWriteEntityCommand { get; }

		public GetBoardRequest(GetBoardByIdOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateGetEntityQuery = () => new GetBoardByIdQuery(opts);
			CreateLogEntityCommand = board => new LogBoardCommand(board, opts);
			CreateWriteEntityCommand = board => new WriteBoardCommand(board, opts);
		}
	}
}
