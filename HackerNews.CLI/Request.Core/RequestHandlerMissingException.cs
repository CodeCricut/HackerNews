using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Request.Core
{
	public class RequestHandlerMissingException : Exception
	{
		private static readonly string MESSAGE = "Request was built before a handler was set.";

		public RequestHandlerMissingException() : base(MESSAGE)
		{

		}

		public RequestHandlerMissingException(string message)
			: base(message)
		{

		}
	}
}
