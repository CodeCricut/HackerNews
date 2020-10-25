using System;

namespace HackerNews.Domain.Errors
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
