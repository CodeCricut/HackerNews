using HackerNews.Domain.Common.Models.Articles;
using MediatR;

namespace Hackernews.WPF.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public ArticlesViewModel ArticleViewModel { get; }
		public NavigationViewModel NavigationViewModel { get; }

		public MainWindowViewModel(IMediator mediator)
		{
			ArticleViewModel = new ArticlesViewModel(new GetArticleModel(), mediator);
			NavigationViewModel = new NavigationViewModel();
		}
	}
}
