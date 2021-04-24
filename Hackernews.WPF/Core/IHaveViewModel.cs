using Hackernews.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Hackernews.WPF.Core
{
	public interface IHaveViewModel<TViewModel>
	{
		void SetViewModel(TViewModel viewModel);
	}
}
