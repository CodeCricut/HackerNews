using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Helpers.Cookies.Interfaces
{
	public interface IJwtService 
	{
		string GetToken();
		void SetToken(string token, int expireseMinutes);
		bool ContainsToken();
	}
}
