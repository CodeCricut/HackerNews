using System.Windows.Input;

namespace HackerNews.WPF.Core.Commands
{
	public delegate ICommand CreateCommand<TViewModel>(TViewModel viewModel);
	public delegate BaseCommand CreateBaseCommand<TViewModel>(TViewModel viewModel);
	public delegate TCommand CreateCommand<TViewModel, TCommand>(TViewModel viewModel);
}
