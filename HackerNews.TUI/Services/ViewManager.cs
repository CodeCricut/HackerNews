using ConsoleFramework;
using ConsoleFramework.Controls;
using HackerNews.WPF.Core.Services;
using HackerNews.WPF.Core.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HackerNews.TUI.Services
{
	class ViewManager : IViewManager
	{
		private readonly ILogger<ViewManager> _logger;
		private readonly WindowsHost _windowsHost;

		private readonly Dictionary<BaseViewModel, Window> _activeViews = new();

		public ViewManager(ILogger<ViewManager> logger, WindowsHost windowsHost)
		{
			_logger = logger;
			_windowsHost = windowsHost;
		}

		public bool Show<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
			if (_activeViews.ContainsKey(viewModel))
				return false;

			try
			{
				string viewName = GetViewName(viewModel);

				string viewFileLoc = $"{viewModel.GetType().GetTypeInfo().Assembly.GetName().Name}.{viewName}.xml"; // TODO: extract to IViewLocator

				var view = (Window)ConsoleApplication.LoadFromXaml(viewFileLoc, viewModel);

				_windowsHost.Show(view);

				_activeViews.Add(viewModel, view);

				return true;
			}
			catch (Exception)
			{
				_logger.LogError($"Could not find and show view for view model {viewModel}.");
				return false;
			}
		}

		private static string GetViewName<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
		{
			string viewName = viewModel.GetType().Name;
			int index = viewName.ToLower().IndexOf("viewmodel");
			if (index > 0)
			{
				viewName = viewName.Remove(index);
			}

			return viewName;
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
