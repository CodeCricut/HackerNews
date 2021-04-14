using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Hackernews.WPF.Core
{
	public delegate ICommand CreateCommand<TViewModel>(TViewModel viewModel);
	public delegate BaseCommand CreateBaseCommand<TViewModel>(TViewModel viewModel);
	public delegate TCommand CreateCommand<TViewModel, TCommand>(TViewModel viewModel);
}
