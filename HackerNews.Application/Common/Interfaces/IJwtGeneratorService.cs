using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.Interfaces
{
	public interface IJwtGeneratorService
	{
		Task<Jwt> GenererateJwtFromLoginModelAsync(LoginModel loginModel);
	}
}
