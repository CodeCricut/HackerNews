using ConsoleFramework;
using ConsoleFramework.Controls;
using HackerNews.WPF.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.TUI.Services
{
	public interface IViewFinder
	{
		Window FindWindowForViewModel(BaseViewModel viewModel);
	}

	class ViewFinder : IViewFinder
	{
		public Window FindWindowForViewModel(BaseViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
			
			string viewName = GetViewName(viewModel);
			string viewFileLoc = $"{viewModel.GetType().Assembly.GetName().Name}.{viewName}.xml"; // TODO: extract to IViewLocator

			return (Window)ConsoleApplication.LoadFromXaml(viewFileLoc, viewModel);
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
	}
}
