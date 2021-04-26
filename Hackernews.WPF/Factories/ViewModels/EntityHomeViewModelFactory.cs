using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IEntityHomeViewModelFactory
	{
		EntityHomeViewModel Create();
	}

	public class EntityHomeViewModelFactory : IEntityHomeViewModelFactory
	{
		private readonly IViewManager _viewManager;

		public EntityHomeViewModelFactory(IViewManager viewManager
			)
		{
			_viewManager = viewManager;
		}

		public EntityHomeViewModel Create()
			=> new EntityHomeViewModel(_viewManager);
	}
}
