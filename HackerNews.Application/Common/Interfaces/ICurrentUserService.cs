namespace HackerNews.Application.Common.Interfaces
{
	/// <summary>
	/// An interface for retrieving the ID of the current user, if logged in.
	/// </summary>
	public interface ICurrentUserService
	{
		int UserId { get; }
	}
}
