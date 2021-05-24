using HackerNews.CLI.Loggers;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.PrintEntity
{
	public class LogEntityCommand<TGetModel> : IRequest
		where TGetModel : GetModelDto
	{
		public LogEntityCommand(TGetModel entity, IPrintOptions options)
		{
			Entity = entity;
			Options = options;
		}

		public TGetModel Entity { get; }
		public IPrintOptions Options { get; }
	}


	public class LogEntityCommandHandler<TGetModel> where TGetModel : GetModelDto
	{
		private readonly IEntityLogger<TGetModel> _entityLogger;

		public LogEntityCommandHandler(IEntityLogger<TGetModel> entityLogger)
		{
			_entityLogger = entityLogger;
		}

		public Task PrintEntity(LogEntityCommand<TGetModel> command)
		{
			if (command.Options.Print)
				_entityLogger.LogEntity(command.Entity);

			return Task.CompletedTask;
		}
	}
}
