using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Request.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.Requests.GetEntityById
{
	public class GetEntityByIdRequest<TGetModel, TInclusionConfig> : IRequest
	{
		private readonly ILogger<GetEntityByIdRequest<TGetModel, TInclusionConfig>> _logger;
		private readonly IVerbositySetter _verbositySetter;
		private readonly IConfigurableEntityLogger<TGetModel, TInclusionConfig> _entityLogger;
		private readonly IConfigurableEntityWriter<TGetModel, TInclusionConfig> _entityWriter;
		private readonly IGetEntityRepository<TGetModel> _getEntityRepository;
		private readonly TInclusionConfig _entityInclusionConfig;
		private readonly bool _verbose;
		private readonly bool _print;
		private readonly string _fileLocation;
		private readonly int _id;

		public GetEntityByIdRequest(
			ILogger<GetEntityByIdRequest<TGetModel, TInclusionConfig>> logger,
			IVerbositySetter verbositySetter,
			IConfigurableEntityLogger<TGetModel, TInclusionConfig> entityLogger,
			IConfigurableEntityWriter<TGetModel, TInclusionConfig> entityWriter,
			IGetEntityRepository<TGetModel> getEntityRepository,

			TInclusionConfig entityInclusionConfig,

			// TODO: pass in as IGetEntityByIdOptions ???
			bool verbose,
			bool print,
			string fileLocation,
			int id)
		{
			_logger = logger;
			_verbositySetter = verbositySetter;
			_entityLogger = entityLogger;
			_entityWriter = entityWriter;
			_getEntityRepository = getEntityRepository;
			_entityInclusionConfig = entityInclusionConfig;
			_verbose = verbose;
			_print = print;
			_fileLocation = fileLocation;
			_id = id;
		}


		public async Task ExecuteAsync()
		{
			_verbositySetter.SetVerbository(_verbose);

			TGetModel entity = await _getEntityRepository.GetByIdAsync(_id);
			if (entity == null)
			{
				_logger.LogWarning($"Could not find a entity with the ID of {_id}. Aborting request...");
				return;
			}

			if (_print)
			{
				_entityLogger.Configure(_entityInclusionConfig);
				_entityLogger.LogEntity(entity);
			}

			if (!string.IsNullOrEmpty(_fileLocation))
			{
				_entityWriter.Configure(_entityInclusionConfig);
				await _entityWriter.WriteEntityAsync(_fileLocation, entity);
			}
		}


		public Task CancelAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
