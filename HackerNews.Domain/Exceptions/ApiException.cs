using System;

namespace HackerNews.Domain.Exceptions
{
	public enum ApiExceptionData
	{
		ERRORS
	}

	/// <summary>
	/// The base exception type for the application.
	/// </summary>
	public abstract class ApiException : Exception
	{
		public ApiException(string message, object errorObject) : base(message)
		{
			// Add the error object to a publicly available dictionary.
			Data[ApiExceptionData.ERRORS] = errorObject;
		}
	}
}
