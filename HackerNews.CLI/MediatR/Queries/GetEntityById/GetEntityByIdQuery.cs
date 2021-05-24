﻿using HackerNews.CLI.Configuration;
using HackerNews.CLI.EntityRepository;
using HackerNews.CLI.FileWriters;
using HackerNews.CLI.Loggers;
using HackerNews.CLI.Options.Verbs;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR
{
	public class GetEntityByIdQuery<TGetModel> : IRequest<TGetModel>
		where TGetModel : GetModelDto
	{
		public GetEntityByIdQuery(IIdOptions options)
		{
			Options = options;
		}

		public IIdOptions Options { get; }
	}

	public class GetEntityByIdQueryHandler<TRequest, TGetModel> : 
		IRequestHandler<TRequest, TGetModel>
		where TRequest : GetEntityByIdQuery<TGetModel>
		where TGetModel : GetModelDto
	{
		private readonly ILogger<GetEntityByIdQueryHandler<TRequest, TGetModel>> _logger;
		private readonly IEntityFinder<TGetModel> _entityFinder;

		public GetEntityByIdQueryHandler(
			ILogger<GetEntityByIdQueryHandler<TRequest, TGetModel>> logger,
			IEntityFinder<TGetModel> entityFinder)
		{
			_logger = logger;
			_entityFinder = entityFinder;
		}

		public Task<TGetModel> Handle(TRequest request, CancellationToken cancellationToken)
		{
			return _entityFinder.GetByIdAsync(request.Options.Id);
		}
	}
}
