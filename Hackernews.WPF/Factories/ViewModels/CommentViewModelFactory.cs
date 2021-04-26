using Hackernews.WPF.MVVM.ViewModel;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface ICommentViewModelFactory
	{
		CommentViewModel Create();
	}

	public class CommentViewModelFactory : ICommentViewModelFactory
	{
		public CommentViewModel Create()
			=> new CommentViewModel();
	}
}
