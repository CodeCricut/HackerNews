namespace Hackernews.WPF.Core
{
	public interface IHaveViewModel<TViewModel>
	{
		void SetViewModel(TViewModel viewModel);
	}
}
