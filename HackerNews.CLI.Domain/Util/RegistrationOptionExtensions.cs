using HackerNews.CLI.Domain.Options;
using HackerNews.Domain.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Domain.Util
{
	public static class RegistrationOptionExtensions
	{
		public static RegisterUserModel ToRegisterUserModel(this IRegisterOptions opts)
		{
			return new RegisterUserModel()
			{
				FirstName = opts.Firstname,
				LastName = opts.Lastname,
				UserName = opts.Username,
				Password = opts.Password
			};
		}
	}
}
