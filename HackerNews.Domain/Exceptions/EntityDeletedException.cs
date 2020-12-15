namespace HackerNews.Domain.Exceptions
{
	public class EntityDeletedException : ApiException
	{
		public EntityDeletedException(string message = "This entity is deleted and you do not have access to it.", object errorObject = null) : base(message, errorObject)
		{
		}
	}
}
