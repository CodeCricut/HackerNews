namespace HackerNews.Domain.Exceptions
{
	public class InvalidPostException : ApiException
	{
		private const string DEFAULT_MESSAGE = "Invalid post request.";
		public InvalidPostException(string message = DEFAULT_MESSAGE,
			object errorObject = null)
			: base(message, errorObject)
		{
		}

		public InvalidPostException(object errorObject = null)
			: base(DEFAULT_MESSAGE, errorObject)
		{
		}
	}
}
