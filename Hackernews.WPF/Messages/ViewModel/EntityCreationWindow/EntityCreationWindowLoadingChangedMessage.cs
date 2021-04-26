using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Messages.ViewModel.EntityCreationWindow
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
