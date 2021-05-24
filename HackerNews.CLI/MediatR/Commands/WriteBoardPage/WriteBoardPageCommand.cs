using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.WriteBoardPage
{
	public class WriteBoardPageCommand : WriteEntityPageCommand<GetBoardModel>
	{
		public WriteBoardPageCommand(PaginatedList<GetBoardModel> entityPage, 
			IFileOptions fileOptions) : base(entityPage, fileOptions)
		{
		}

		public class WriteBoardPageCommandHandler : WriteEntityPageCommandHandler<GetBoardModel>,
			IRequestHandler<WriteBoardPageCommand>
		{
			public WriteBoardPageCommandHandler(IEntityWriter<GetBoardModel> entityWriter) : base(entityWriter)
			{
			}

			public Task<Unit> Handle(WriteBoardPageCommand request, CancellationToken cancellationToken)
			{
				base.WriteEntityPageAsync(request);

				return Unit.Task;
			}
		}
	}
}
