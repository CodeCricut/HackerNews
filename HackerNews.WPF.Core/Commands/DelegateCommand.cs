using System;
using System.Windows.Input;

namespace HackerNews.WPF.Core.Commands
{
	public class DelegateCommand : ICommand, ConsoleFramework.Events.ICommand
	{
		private readonly Action<object> _execute;
		private readonly Func<bool> _canExecute;

		public DelegateCommand(Action<object> execute, Func<bool> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged;

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute == null ? true : _canExecute();
		}

		public void Execute(object parameter = null)
		{
			_execute?.Invoke(parameter);
		}
	}
}
