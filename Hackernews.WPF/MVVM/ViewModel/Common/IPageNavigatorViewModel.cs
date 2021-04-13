using Hackernews.WPF.Helpers;

namespace Hackernews.WPF.MVVM.ViewModel.Common
{
	public interface IPageNavigatorViewModel
	{
		public int CurrentPage { get; }
		public int TotalPages { get; }

		AsyncDelegateCommand PrevPageCommand { get; }
		AsyncDelegateCommand NextPageCommand { get; }
	}
}
