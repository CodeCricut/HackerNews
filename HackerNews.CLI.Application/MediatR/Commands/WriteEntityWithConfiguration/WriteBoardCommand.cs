using HackerNews.CLI.FileWriters;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.MediatR.Commands.WriteEntity;
using HackerNews.CLI.Util;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Application.MediatR.Commands.WriteEntityWithConfiguration
{
	public class WriteBoardCommand : WriteEntityCommand<GetBoardModel, BoardInclusionConfiguration>
	{
		public WriteBoardCommand(GetBoardModel entity, IFileOptions options, BoardInclusionConfiguration inclusionConfig) : base(entity, options, inclusionConfig)
		{
		}
	}

	public class WriteBoardCommandHandler :
		WriteEntityCommandHandler<WriteBoardCommand, GetBoardModel, BoardInclusionConfiguration>
	{
		public WriteBoardCommandHandler(IConfigurableEntityWriter<GetBoardModel, BoardInclusionConfiguration> configEntityWriter) : base(configEntityWriter)
		{
		}
	}
}
