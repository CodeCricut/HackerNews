using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.WriteBoardPage
{
	public class WriteBoardPageWithConfigurationCommand : WriteEntityPageCommand<GetBoardModel>
	{
		public WriteBoardPageWithConfigurationCommand(PaginatedList<GetBoardModel> entityPage,
			IFileOptions fileOptions,
			IBoardInclusionOptions inclusionOptions) : base(entityPage, fileOptions)
		{
			InclusionOptions = inclusionOptions;
		}

		public IBoardInclusionOptions InclusionOptions { get; }
	}

	public class WriteBoardPageWithConfigurationCommandHandler :
		WriteEntityPageCommandHandler<WriteBoardPageWithConfigurationCommand, GetBoardModel>
	{
		private readonly IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> _entityWriter;

		public WriteBoardPageWithConfigurationCommandHandler(
			IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> entityWriter) : base(entityWriter)
		{
			_entityWriter = entityWriter;
		}

		public override Task<Unit> Handle(WriteBoardPageWithConfigurationCommand request, CancellationToken cancellationToken)
		{
			_entityWriter.Configure(request.InclusionOptions.ToInclusionConfiguration());
			return base.Handle(request, cancellationToken);
		}
	}
}
