using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.WriteEntity
{
	public class WriteEntityCommand<TGetModel> : IRequest
		where TGetModel : GetModelDto
	{
		public WriteEntityCommand(TGetModel entity, IFileOptions options)
		{
			Entity = entity;
			Options = options;
		}

		public TGetModel Entity { get; }
		public IFileOptions Options { get; }
	}

	public class WriteEntityCommandHandler<TGetModel> 
		where TGetModel : GetModelDto
	{
		private readonly IEntityWriter<TGetModel> _entityWriter;

		public WriteEntityCommandHandler(IEntityWriter<TGetModel> entityWriter)
		{
			_entityWriter = entityWriter;
		}

		public Task WriteEntity(WriteEntityCommand<TGetModel> request)
		{
			if (!string.IsNullOrEmpty(request.Options.FileLocation))
				return _entityWriter.WriteEntityAsync(request.Options.FileLocation, request.Entity);

			return Task.CompletedTask;
		}
	}
}
