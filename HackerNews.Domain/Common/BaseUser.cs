using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Common
{
	public class BaseUser : DomainEntity
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
