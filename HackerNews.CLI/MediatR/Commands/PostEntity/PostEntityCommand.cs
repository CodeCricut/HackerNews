using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.CLI.MediatR.Commands.PostEntity
{
	public class PostEntityCommand<TPostModel, TGetModel> : IRequest<TGetModel>
		where TPostModel : PostModelDto
		where TGetModel : GetModelDto
	{
		public PostEntityCommand(TPostModel postModel)
		{
			PostModel = postModel;
		}

		public TPostModel PostModel { get; }
	}

	public class PostEntityCommandHandler<TPostModel, TGetModel>
		where TPostModel : PostModelDto
		where TGetModel : GetModelDto
	{
		private readonly IEntityApiClient<TPostModel, TGetModel> _entityApiClient;

		public PostEntityCommandHandler(IEntityApiClient<TPostModel, TGetModel> entityApiClient)
		{
			_entityApiClient = entityApiClient;
		}

		public Task<TGetModel> PostEntity(PostEntityCommand<TPostModel, TGetModel> request)
		{
			return _entityApiClient.PostAsync(request.PostModel);
		}
	}
}
