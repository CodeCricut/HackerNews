using HackerNews.WPF.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.WPF.Core.Services
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
}
