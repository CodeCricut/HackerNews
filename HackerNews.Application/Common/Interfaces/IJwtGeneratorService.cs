using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.Interfaces
{
	public interface IJwtGeneratorService
	{
		Task<Jwt> GenererateJwtFromLoginModelAsync(LoginModel loginModel);
	}
}
