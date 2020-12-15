namespace HackerNews.Domain.Common
{
	/// <summary>
	/// The various policies on who can access deleted entities, and what properties to expose of deleted entities.
	/// </summary>
	public enum DeletedEntityPolicy
	{
		// Not accessible to anyone
		INACCESSIBLE,
		// Only accessible to the creator or some other owner (like a moderator)
		OWNER,
		// Accessible to everyone
		PUBLIC,
		// Only some properties are accessible to everyone
		RESTRICTED
	}
}
