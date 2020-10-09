using HackerNews.Domain;
using HackerNews.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.JWT
{
	public interface IJwtHelper
	{
		void AttachJwtToken(ref GetPrivateUserModel privateUser, string jwtToken);
		string GenerateJwtToken(User user);
	}
}
