using Hackernews.WPF.Core;
using Hackernews.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Hackernews.WPF.Services
{
	public interface IViewManager
	{
		void Show(BaseViewModel viewModel);
	}

	public class ViewManager : IViewManager
	{
		public void Show(BaseViewModel viewModel)
		{
			Type viewType = GetViewForViewModel(viewModel.GetType());

			var view = (Window) (App.Current as App).ServiceProvider.GetService(viewType);

			view.Show();
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
					
				
				//view => 
				//view.GetInterfaces().FirstOrDefault(t => 
				//	t.GenericTypeArguments
				//		black .Contains(viewModelType)) != null).FirstOrDefault();

			return viewForVM;
		}
	}
}
