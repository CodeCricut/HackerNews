using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.MediatR.Commands.WriteEntityPage;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntityPageWithConfiguration
{
	public class WriteBoardPageCommand : WriteEntityPageCommand<GetBoardModel, BoardInclusionConfiguration>
	{
		public WriteBoardPageCommand(PaginatedList<GetBoardModel> entityPage, IFileOptions fileOptions, BoardInclusionConfiguration inclusionConfig) : base(entityPage, fileOptions, inclusionConfig)
		{
		}
	}

	public class WriteBoardPageCommandHandler :
		WriteEntityPageCommandHandler<WriteBoardPageCommand, GetBoardModel, BoardInclusionConfiguration>
	{
		public WriteBoardPageCommandHandler(IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> configEntityWriter) : base(configEntityWriter)
		{
		}
	}
}
