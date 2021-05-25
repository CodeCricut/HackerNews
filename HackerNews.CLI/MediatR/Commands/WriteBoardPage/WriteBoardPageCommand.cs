using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.MediatR.Commands.WriteBoardPage
{
	public class WriteBoardPageCommand : WriteEntityPageCommand<GetBoardModel>
	{
		public WriteBoardPageCommand(PaginatedList<GetBoardModel> entityPage,
			IFileOptions fileOptions) : base(entityPage, fileOptions)
		{
		}

		public class WriteBoardPageCommandHandler :
			WriteEntityPageCommandHandler<WriteBoardPageCommand, GetBoardModel>
		{
			public WriteBoardPageCommandHandler(IEntityWriter<GetBoardModel> entityWriter) : base(entityWriter)
			{
			}
		}
	}
}
