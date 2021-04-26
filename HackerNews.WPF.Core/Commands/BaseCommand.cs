using System;
using System.Windows.Input;

namespace HackerNews.WPF.Core.Commands
{
	public abstract class BaseCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public virtual bool CanExecute(object parameter) => true;
		public abstract void Execute(object parameter);

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		public void TryExecute(object parameter = null)
		{
			if (CanExecute(parameter)) Execute(parameter);
		}
	}
}
