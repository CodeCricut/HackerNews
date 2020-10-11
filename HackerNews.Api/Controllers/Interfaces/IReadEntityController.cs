using CleanEntityArchitecture.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Interfaces
{
	interface IReadEntityController<TEntity, TGetModel>
		where TEntity : DomainEntity
		where TGetModel : GetModelDto
	{
		Task<ActionResult<PagedList<TGetModel>>> GetAsync([FromQuery] PagingParams pagingParams);

		Task<ActionResult<TGetModel>> GetByIdAsync(int key);
	}
}
