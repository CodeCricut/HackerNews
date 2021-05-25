using HackerNews.Domain.Common;

namespace HackerNews.CLI.ApplicationRequests.GetEntitiesRequests
{
	public interface IGetEntitiesRequest<TGetModel>
		where TGetModel : GetModelDto
	{
		CreateVerbosityCommand CreateVerbosityCommand { get; }
		CreateGetEntitiesByIdsQuery<TGetModel> CreateGetEntitiesByIdsQuery { get; }
		CreateLogEntityPageCommand<TGetModel> CreateLogEntityPageCommand { get; }
		CreateWriteEntityPageCommand<TGetModel> CreateWriteEntityPageCommand { get; }
	}
}
