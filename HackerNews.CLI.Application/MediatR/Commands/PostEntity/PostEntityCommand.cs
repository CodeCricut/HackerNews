using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common;
using MediatR;
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

	// TODO: having three generic args is pretty unweildly
	public class PostEntityCommandHandler<TRequest, TPostModel, TGetModel> :
		IRequestHandler<TRequest, TGetModel>
		where TRequest : PostEntityCommand<TPostModel, TGetModel>
		where TPostModel : PostModelDto
		where TGetModel : GetModelDto
	{
		private readonly IEntityApiClient<TPostModel, TGetModel> _entityApiClient;

		public PostEntityCommandHandler(IEntityApiClient<TPostModel, TGetModel> entityApiClient)
		{
			_entityApiClient = entityApiClient;
		}

		public virtual Task<TGetModel> Handle(TRequest request, CancellationToken cancellationToken)
		{
			return _entityApiClient.PostAsync(request.PostModel);
		}
	}
}
