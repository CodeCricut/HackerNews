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
		private readonly IBoardViewModelFactory _boardViewModelFactory;
		private readonly IBoardHomeViewModelFactory _boardHomeViewModelFactory;
		private readonly IArticleViewModelFactory _articleViewModelFactory;
		private readonly IArticleHomeViewModelFactory _articleHomeViewModelFactory;

		public EntityHomeViewModelFactory(IViewManager viewManager,
			IBoardViewModelFactory boardViewModelFactory,
			IBoardHomeViewModelFactory boardHomeViewModelFactory,
			IArticleViewModelFactory articleViewModelFactory,
			IArticleHomeViewModelFactory articleHomeViewModelFactory
			)
		{
			_viewManager = viewManager;
			_boardViewModelFactory = boardViewModelFactory;
			_boardHomeViewModelFactory = boardHomeViewModelFactory;
			_articleViewModelFactory = articleViewModelFactory;
			_articleHomeViewModelFactory = articleHomeViewModelFactory;
		}

		public EntityHomeViewModel Create()
			=> new EntityHomeViewModel(_viewManager, _articleViewModelFactory, _articleHomeViewModelFactory);
	}
}
