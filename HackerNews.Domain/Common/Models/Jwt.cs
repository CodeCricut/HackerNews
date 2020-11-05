using System;

namespace HackerNews.Domain.Common.Models
{
	public class Jwt
	{
		public DateTime Expires { get; set; }
		public string Token { get; set; }

		// For deserialization.
		public Jwt()
		{

		}

		public Jwt(DateTime expires, string token)
		{
			Expires = expires;
			Token = token;
		}
	}
}
