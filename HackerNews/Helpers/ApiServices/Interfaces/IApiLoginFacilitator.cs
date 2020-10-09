using HackerNews.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiLoginFacilitator<TLoginModel, TGetModel>
		where TGetModel : GetEntityModel
	{
		Task<TGetModel> GetUserByCredentialsAsync(TLoginModel authUserReq);
	}
}
