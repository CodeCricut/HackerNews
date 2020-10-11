using HackerNews.Domain;
using HackerNews.Domain.Models.Users;

namespace HackerNews.Api.Helpers.JWT
{
	public interface IJwtHelper
	{
		void AttachJwtToken(ref GetPrivateUserModel privateUser, string jwtToken);
		string GenerateJwtToken(User user);
	}
}
