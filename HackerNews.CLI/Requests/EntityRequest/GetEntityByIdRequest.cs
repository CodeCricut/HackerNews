using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Options.Verbs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Requests.EntityRequest
{
	public class GetEntityByIdRequest<TGetModel> : IRequest
	{
		private readonly ILogger<GetEntityByIdRequest<TGetModel>> _logger;
		private readonly IVerbositySetter _verbositySetter;
		private readonly IEntityLogger<TGetModel> _entityLogger;
		private readonly IEntityWriter<TGetModel> _entityWriter;
		private readonly IGetEntityRepository<TGetModel> _getEntityRepo;
		private readonly IGetEntityByIdOptions _options;

		public GetEntityByIdRequest(
			ILogger<GetEntityByIdRequest<TGetModel>> logger,
			// TODO: Since this is a shared convern, I feel like it should not be here
			IVerbositySetter verbositySetter,
			
			IEntityLogger<TGetModel> entityLogger,
			IEntityWriter<TGetModel> entityWriter,
			IGetEntityRepository<TGetModel> getEntityRepo,
			IGetEntityByIdOptions options
			)
		{
			_logger = logger;
			_verbositySetter = verbositySetter;
			_entityLogger = entityLogger;
			_entityWriter = entityWriter;
			_getEntityRepo = getEntityRepo;
			_options = options;
		}

		public async Task ExecuteAsync()
		{
			_verbositySetter.SetVerbository(_options.Verbose);

			TGetModel entity = await _getEntityRepo.GetByIdAsync(_options.Id);
			if (entity == null)
			{
				_logger.LogWarning($"Could not find a entity with the ID of {_options.Id}. Aborting request...");
				return;
			}

			if (_options.Print)
				_entityLogger.LogEntity(entity);

			if (!string.IsNullOrEmpty(_options.FileLocation))
				await _entityWriter.WriteEntityAsync(_options.FileLocation, entity);
		}

		public Task CancelAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
