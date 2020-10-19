using CleanEntityArchitecture.Domain;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiLoginFacilitator<TLoginModel, TGetModel>
		where TGetModel : GetModelDto
	{
		Task<Jwt> LogIn(TLoginModel authUserReq);
	}
}
