using HackerNews.Domain.Common;

namespace HackerNews.CLI.ApplicationRequests.GetEntityRequests
{
	public interface IGetEntityRequest<TGetModel>
		where TGetModel : GetModelDto
	{
		CreateVerbosityCommand CreateVerbosityCommand { get; }
		CreateGetEntityQuery<TGetModel> CreateGetEntityQuery { get; }
		CreateLogEntityCommand<TGetModel> CreateLogEntityCommand { get; }
		CreateWriteEntityCommand<TGetModel> CreateWriteEntityCommand { get; }
	}
}
