using HackerNews.Domain.Common;

namespace HackerNews.CLI.ApplicationRequests.GetEntitiesRequests
{
	public interface IGetEntitiesRequest<TGetModel, TInclusionConfiguration>
		where TGetModel : GetModelDto
	{
		CreateVerbosityCommand CreateVerbosityCommand { get; }
		CreateGetEntitiesByIdsQuery<TGetModel> CreateGetEntitiesByIdsQuery { get; }
		CreateLogEntityPageCommand<TGetModel, TInclusionConfiguration> CreateLogEntityPageCommand { get; }
		CreateWriteEntityPageCommand<TGetModel, TInclusionConfiguration> CreateWriteEntityPageCommand { get; }
	}
}
