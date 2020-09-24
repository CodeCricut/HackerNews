namespace HackerNews.Domain.Errors
{
	public class NotFoundException : ApiException
	{
		public NotFoundException(string message = "Requested resource not found", object errorObject = null) : base(message, errorObject)
		{
		}
	}
}
