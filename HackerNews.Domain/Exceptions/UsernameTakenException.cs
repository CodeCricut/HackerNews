using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Exceptions
{
	public class UsernameTakenException : ApiException
	{
		public UsernameTakenException(string message = "Username taken.", object errorObject = null) : base(message, errorObject)
		{
		}
	}
}
