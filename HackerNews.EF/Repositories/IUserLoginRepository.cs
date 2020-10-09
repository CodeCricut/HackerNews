using HackerNews.Domain;
using System.Threading.Tasks;

namespace HackerNews.EF.Repositories
{
	public interface IUserLoginRepository
	{
		Task<User> GetUserByCredentialsAsync(string username, string password);
	}
}
