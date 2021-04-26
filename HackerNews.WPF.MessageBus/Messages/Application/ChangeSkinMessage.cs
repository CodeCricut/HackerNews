using HackerNews.WPF.Core;

namespace HackerNews.WPF.MessageBus.Messages.Application
{
	/// <summary>
	/// This event is fired when a change/assignment to the application skin is made.
	/// </summary>
	public sealed class ChangeSkinMessage
	{
		public ChangeSkinMessage(Skin newSkin)
		{
			NewSkin = newSkin;
		}

		public Skin NewSkin { get; }
	}
}
