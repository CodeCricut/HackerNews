using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiUserSaver<TSavedType, TGetModel>
	{
		Task<TGetModel> SaveEntityToUserAsync(int entityId);
	}
}
