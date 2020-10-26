namespace HackerNews.Domain.Exceptions
{
	public class UnauthorizedException : ApiException
	{
		public UnauthorizedException(string message = "Unauthorized to access the requested resource.",
			object errorObject = null) : base(message, errorObject)
		{
		}
	}
}
