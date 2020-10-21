using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiUserSaver<TSavedType>
	{
		public Task SaveEntityToUserAsync(int entityId);
	}
}
