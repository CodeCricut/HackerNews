using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Exceptions
{
	public class BoardTitleTakenException : ApiException
	{
		public BoardTitleTakenException(string message = "This board title is already taken. Board titles must be unique.", object errorObject = null) : base(message, errorObject)
		{
		}
	}
}
