namespace HackerNews.WPF.Core.View
{
	public interface IHaveViewModel<TViewModel>
	{
		void SetViewModel(TViewModel viewModel);
	}
}
