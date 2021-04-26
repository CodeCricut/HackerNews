using Hackernews.WPF.Helpers;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using System.Windows.Input;

namespace Hackernews.WPF.Core
{
	public class WindowViewModel : BaseViewModel
	{
		private readonly IViewManager _viewManager;

		public bool IsClosed { get; private set; } = true;

		public ICommand CloseCommand { get; }

		public WindowViewModel(IViewManager viewManager)
		{
			_viewManager = viewManager;

			CloseCommand = new DelegateCommand(_ => Close());
		}

		public void Open()
		{
			IsClosed = false;
			_viewManager.Show(this);
		}

		public void Close()
		{
			IsClosed = true;
			_viewManager.Close(this);
		}
	}
}
