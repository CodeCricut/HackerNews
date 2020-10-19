using CleanEntityArchitecture.Domain;

namespace HackerNews.Helpers.Cookies.Interfaces
{
	public interface IJwtService
	{
		string GetToken();
		void SetToken(Jwt token, int expireseMinutes);
		bool ContainsToken();
		void RemoveToken();
	}
}
