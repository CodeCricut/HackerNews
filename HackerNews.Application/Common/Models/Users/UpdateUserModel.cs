using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Common.Models.Users
{
	public class UpdateUserModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
	}
}
