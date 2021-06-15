namespace HackerNews.WPF.MessageBus.Messages.ViewModel.EntityCreationWindow
{
	public sealed class EntityCreationWindowInvalidInputChangedMessage
	{
		public EntityCreationWindowInvalidInputChangedMessage(bool invalidInput)
		{
			InvalidInput = invalidInput;
		}

		public bool InvalidInput { get; }
	}
}
