using HackerNews.Domain.Common.Models;

namespace HackerNews.Mvc.Services.Interfaces
{
	/// <summary>
	/// Used to read and write JWT tokens. Implementations include JWT cookies.
	/// </summary>
	public interface IJwtSetterService
	{
		string GetToken();
		void SetToken(Jwt token, int expiresMinutes);
		bool ContainsToken();
		void RemoveToken();
	}
}
