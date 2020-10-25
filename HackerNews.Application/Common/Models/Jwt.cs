using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Common.Models
{
	public class Jwt
	{
		public DateTime Expires { get; set; }
		public string Token { get; set; }

		public Jwt(DateTime expires, string token)
		{
			Expires = expires;
			Token = token;
		}
	}
}
