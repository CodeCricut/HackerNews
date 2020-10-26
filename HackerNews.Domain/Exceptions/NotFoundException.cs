namespace HackerNews.Domain.Exceptions
{
	public class NotFoundException : ApiException
	{
		public NotFoundException(string message = "Requested resource not found", object errorObject = null) : base(message, errorObject)
		{
		}
	}
}
