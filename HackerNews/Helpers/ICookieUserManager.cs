using HackerNews.Domain.Models.Users;
using System.Threading.Tasks;

namespace HackerNews.Helpers
{
	public interface ICookieUserManager
	{
		Task LogInAsync(GetPrivateUserModel user);
		Task LogOutAsync();
	}
}
