using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.MediatR.Commands.WriteEntity
{
	public class WriteBoardCommand : WriteEntityCommand<GetBoardModel>
	{
		public WriteBoardCommand(GetBoardModel entity, IFileOptions options) : base(entity, options)
		{
		}
	}

	public class WriteBoardCommandHandler :
		WriteEntityCommandHandler<WriteBoardCommand, GetBoardModel>
	{
		public WriteBoardCommandHandler(IEntityWriter<GetBoardModel> entityWriter) : base(entityWriter)
		{
		}
	}
}
