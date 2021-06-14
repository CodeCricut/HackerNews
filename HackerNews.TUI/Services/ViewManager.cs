//using ConsoleFramework;
//using ConsoleFramework.Controls;
//using HackerNews.WPF.Core.ViewModel;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Reflection;

//namespace HackerNews.TUI.Services
//{
//	class ViewManager : IViewManager
//	{
//		private readonly ILogger<ViewManager> _logger;

//		private readonly Dictionary<BaseViewModel, Window> _activeViews = new();

//		public ViewManager(ILogger<ViewManager> logger)
//		{
//			_logger = logger;
//		}

//		public bool Show<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
//		{
//			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
//			if (_activeViews.ContainsKey(viewModel))
//				return false;

//			try
//			{
//				string viewName = viewModel.GetType().Name;

//				string viewFileLoc = $"{Assembly.GetExecutingAssembly().FullName}.{viewName}.xml"; // TODO: extract to IViewLocator

//				var view = (Window)ConsoleApplication.LoadFromXaml(viewFileLoc, viewModel);

//				GetWindowsHost().Show(view);

//				_activeViews.Add(viewModel, view);
				
//				return true;
//			}
//			catch (Exception)
//			{
//				_logger.LogError($"Could not find and show view for view model {viewModel}.");
//				return false;
//			}
//		}

//		public bool Close(BaseViewModel viewModel)
//		{
//			if (!_activeViews.ContainsKey(viewModel)) return false;

//			var view = _activeViews.GetValueOrDefault(viewModel);

//			GetWindowsHost().CloseWindow(view);

//			return _activeViews.Remove(viewModel);
//		}

//		private WindowsHost GetWindowsHost()
//		{
//			return (WindowsHost)ConsoleApplication.LoadFromXaml("HackerNews.TUI.windows-host.xml", null); // TODO: load with config file and/or reflection.
//		}
//	}
//}
