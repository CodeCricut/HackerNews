using HackerNews.Domain.Common;

namespace HackerNews.CLI.ApplicationRequests.GetEntityRequests
{
	public interface IGetEntityRequest<TGetModel, TInclusionConfiguration>
		where TGetModel : GetModelDto
	{
		CreateVerbosityCommand CreateVerbosityCommand { get; }
		CreateGetEntityQuery<TGetModel> CreateGetEntityQuery { get; }
		CreateLogEntityCommand<TGetModel, TInclusionConfiguration> CreateLogEntityCommand { get; }
		CreateWriteEntityCommand<TGetModel, TInclusionConfiguration> CreateWriteEntityCommand { get; }
	}
}
