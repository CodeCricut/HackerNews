using HackerNews.Domain.Common;

namespace HackerNews.CLI.ApplicationRequests
{
	public interface IPostEntityRequest<TPostModel, TGetModel>
		where TPostModel : PostModelDto
		where TGetModel : GetModelDto
	{
		CreateVerbosityCommand CreateVerbosityCommand { get; }
		CreateSignInCommand CreateSignInCommand { get; }
		CreatePostCommand<TPostModel, TGetModel> CreatePostCommand { get; }
		CreateLogEntityCommand<TGetModel> CreateLogCommand { get; }
		CreateWriteEntityCommand<TGetModel> CreateWriteCommand { get; }
	}
}
