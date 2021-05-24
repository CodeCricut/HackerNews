using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common.Models.Boards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.WriteEntity
{
	public class WriteBoardCommand : WriteEntityCommand<GetBoardModel>
	{
		public WriteBoardCommand(GetBoardModel entity, IFileOptions options) : base(entity, options)
		{
		}
	}

	public class WriteBoardCommandHandler : WriteEntityCommandHandler<GetBoardModel>,
		IRequestHandler<WriteBoardCommand>
	{
		public WriteBoardCommandHandler(IEntityWriter<GetBoardModel> entityWriter) : base(entityWriter)
		{
		}

		public Task<Unit> Handle(WriteBoardCommand request, CancellationToken cancellationToken)
		{
			base.WriteEntity(request);

			return Unit.Task;
		}
	}
}
