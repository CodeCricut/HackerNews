using HackerNews.Domain.Common.Models.Articles;
using MediatR;

namespace Hackernews.WPF.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public ArticlesViewModel ArticlesViewModel { get; }
		public NavigationViewModel NavigationViewModel { get; }

		public MainWindowViewModel(IMediator mediator)
		{
			ArticlesViewModel = new ArticlesViewModel(new GetArticleModel(), mediator);
			NavigationViewModel = new NavigationViewModel();
		}
	}
}
