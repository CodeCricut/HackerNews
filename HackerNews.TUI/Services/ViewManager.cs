using ConsoleFramework.Controls;
using HackerNews.WPF.Core.Services;
using HackerNews.WPF.Core.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace HackerNews.TUI.Services
{
	class ViewManager : IViewManager
	{
		private readonly ILogger<ViewManager> _logger;
		private readonly WindowsHost _windowsHost;
		private readonly IViewFinder _viewFinder;

		private readonly Dictionary<BaseViewModel, Window> _activeViews = new();

		public ViewManager(ILogger<ViewManager> logger, WindowsHost windowsHost, IViewFinder viewFinder)
		{
			_logger = logger;
			_windowsHost = windowsHost;
			_viewFinder = viewFinder;
		}

		public bool Show<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
			if (_activeViews.ContainsKey(viewModel))
				return false;

			try
			{
				var window = _viewFinder.FindWindowForViewModel(viewModel);

				_windowsHost.Show(window);

				_activeViews.Add(viewModel, window);

				return true;
			}
			catch (Exception)
			{
				_logger.LogError($"Could not find and show view for view model {viewModel}.");
				return false;
			}
		}
		

		public bool Close(BaseViewModel viewModel)
		{
			if (!_activeViews.ContainsKey(viewModel)) return false;

			var view = _activeViews.GetValueOrDefault(viewModel);

			_windowsHost.CloseWindow(view);

			return _activeViews.Remove(viewModel);
		}
	}
}
