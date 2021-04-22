using Hackernews.WPF.Helpers;
using Hackernews.WPF.ViewModels;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Common
{
	public abstract class BaseEntityViewModel : BaseViewModel
	{
		public AsyncDelegateCommand LoadEntityCommand { get; protected set; } = new AsyncDelegateCommand(_ => Task.CompletedTask, _ => false);
	}
}
