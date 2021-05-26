using HackerNews.Domain.Common;
using MediatR;
using System.Threading.Tasks;

namespace HackerNews.CLI.ApplicationRequests
{
	public interface IPostEntityRequestHandler<TPostModel, TGetModel>
		where TPostModel : PostModelDto
		where TGetModel : GetModelDto
	{
		Task HandleAsync(IPostEntityRequest<TPostModel, TGetModel> request);
	}

	public class PostEntityRequestHandler<TPostModel, TGetModel> : 
		IPostEntityRequestHandler<TPostModel, TGetModel>
		where TPostModel : PostModelDto
		where TGetModel : GetModelDto
	{
		private readonly IMediator _mediator;

		public PostEntityRequestHandler(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task HandleAsync(IPostEntityRequest<TPostModel, TGetModel> request)
		{
			await _mediator.Send(request.CreateVerbosityCommand());

			bool signInSuccessful = await _mediator.Send(request.CreateSignInCommand());
			if (!signInSuccessful) return;

			var postedBoard = await _mediator.Send(request.CreatePostCommand());

			await _mediator.Send(request.CreateLogCommand(postedBoard));
			await _mediator.Send(request.CreateWriteCommand(postedBoard));
		}
	}
}
