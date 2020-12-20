using HackerNews.Domain.Common.Models.Users;
using System.Threading.Tasks;

namespace HackerNews.Mvc.Services.Interfaces
{
	/// <summary>
	/// Responsible for logging the user in and out.
	/// </summary>
	public interface IUserAuthService
	{
		Task LogInAsync(LoginModel loginModel);
		Task LogOutAsync();
	}
}
