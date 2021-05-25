using HackerNews.CLI.MediatR;
using HackerNews.CLI.MediatR.Queries.GetEntitiesByIds;
using HackerNews.Domain.Common;

namespace HackerNews.CLI.ApplicationRequests
{
	public delegate GetEntityByIdQuery<TGetModel> CreateGetEntityQuery<TGetModel>()
		where TGetModel : GetModelDto;

	public delegate GetEntitiesByIdsQuery<TGetModel> CreateGetEntitiesByIdsQuery<TGetModel>()
		where TGetModel : GetModelDto;
}
