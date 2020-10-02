using HackerNews.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.EF.Repositories
{
	public interface IUserRepository
	{
		Task<User> GetUserByCredentialsAsync(string username, string password);
	}
}
