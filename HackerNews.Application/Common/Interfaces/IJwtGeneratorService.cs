using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Entities;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.Interfaces
{
	public interface IJwtGeneratorService
	{
		Task<Jwt> GenererateJwtFromUser(User user);
	}
}
