namespace HackerNews.Domain.Exceptions
{
	public class BoardTitleTakenException : ApiException
	{
		public BoardTitleTakenException(string message = "This board title is already taken. Board titles must be unique.", object errorObject = null) : base(message, errorObject)
		{
		}
	}
}
