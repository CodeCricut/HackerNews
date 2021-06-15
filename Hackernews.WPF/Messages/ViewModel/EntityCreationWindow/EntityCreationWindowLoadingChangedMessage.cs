namespace HackerNews.WPF.MessageBus.Messages.ViewModel.EntityCreationWindow
{
	public sealed class EntityCreationWindowLoadingChangedMessage
	{
		public EntityCreationWindowLoadingChangedMessage(bool isLoading)
		{
			IsLoading = isLoading;
		}

		public bool IsLoading { get; }
	}
}
