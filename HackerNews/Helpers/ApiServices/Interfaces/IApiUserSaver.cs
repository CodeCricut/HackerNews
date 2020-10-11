using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiUserSaver<TSavedType, TGetModel>
	{
		Task<TGetModel> SaveEntityToUserAsync(int entityId);
	}
}
