using HackerNews.Application.Common.Models;
using HackerNews.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.Interfaces
{
	public interface IReadEntityController<TEntity, TGetModel>
		where TEntity : DomainEntity
		where TGetModel : GetModelDto
	{
		Task<ActionResult<PaginatedList<TGetModel>>> GetAsync([FromQuery] PagingParams pagingParams);
		Task<ActionResult<TGetModel>> GetByIdAsync(int key);
	}
}
