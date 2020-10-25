namespace HackerNews.Domain.Errors
{
	public class UnauthorizedException : ApiException
	{
		public UnauthorizedException(string message = "Unauthorized to access the requested resource.",
			object errorObject = null) : base(message, errorObject)
		{
		}
	}
}
