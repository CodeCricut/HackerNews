using System;

namespace HackerNews.Domain.Exceptions
{
	public enum ApiExceptionData
	{
		ERRORS
	}

	public abstract class ApiException : Exception
	{
		public ApiException(string message, object errorObject) : base(message)
		{
			Data[ApiExceptionData.ERRORS] = errorObject;
		}
	}
}
