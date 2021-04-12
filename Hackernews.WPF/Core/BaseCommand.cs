using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Hackernews.WPF.Core
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
