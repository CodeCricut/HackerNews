﻿using HackerNews.WPF.Core.View;
using HackerNews.WPF.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Hackernews.WPF.Services
{
	public interface IViewManager
	{
		/// <summary>
		/// Show the view associated with <paramref name="viewModel"/>.
		/// </summary>
		/// <param name="viewModel"></param>
		/// <returns>Successful.</returns>
		bool Show<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel;

		/// <summary>
		/// Close the view associated with <paramref name="viewModel"/>. <see cref="Show(BaseViewModel)"/> must have been previously
		/// called with the same <paramref name="viewModel"/> reference.
		/// </summary>
		/// <param name="viewModel"></param>
		/// <returns>Successful.</returns>
		bool Close(BaseViewModel viewModel);
	}

	public class ViewManager : IViewManager
	{
		private readonly Dictionary<BaseViewModel, Window> _activeViews = new Dictionary<BaseViewModel, Window>();

		public bool Show<TViewModel>(TViewModel viewModel)
			where TViewModel : BaseViewModel
		{
			if (_activeViews.ContainsKey(viewModel))
				return false;
			Type viewType = GetViewForViewModel(viewModel.GetType());

			return Application.Current.Dispatcher.Invoke(() =>
			{
				// Ah a code smell appears, but I know not how to dispel it!
				var view = (Window)(App.Current as App).ServiceProvider.GetService(viewType);
				if (view == null) return false;

				((IHaveViewModel<TViewModel>)view).SetViewModel(viewModel);
				view.Show();

				_activeViews.Add(viewModel, view);

				return true;
			});
		}

		public bool Close(BaseViewModel viewModel)
		{
			return Application.Current.Dispatcher.Invoke(() =>
			{
				if (!_activeViews.ContainsKey(viewModel)) return false;

				var view = _activeViews.GetValueOrDefault(viewModel);

				view?.Close();

				return _activeViews.Remove(viewModel);
			});
		}

		private Type GetViewForViewModel(Type viewModelType)
		{
			var types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes());

			var viewForVM = types
				.FirstOrDefault(viewType => viewType
					.GetInterfaces()
						.Any(i =>
							i.IsGenericType &&
							i.GetGenericTypeDefinition() == typeof(IHaveViewModel<>) &&
							i.GenericTypeArguments.Contains(viewModelType)));

			return viewForVM;
		}
	}
}
