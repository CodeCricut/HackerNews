using Hackernews.WPF.Factories.Commands;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IArticleListViewModelFactory
	{
		ArticleListViewModel Create(LoadEntityListEnum loadEntityType);
	}

	public class ArticleListViewModelFactory : IArticleListViewModelFactory
	{
		private readonly ILoadArticlesCommandFactory _loadArticlesCommandFactory;

		public ArticleListViewModelFactory(ILoadArticlesCommandFactory loadArticlesCommandFactory)
		{
			_loadArticlesCommandFactory = loadArticlesCommandFactory;
		}

		public ArticleListViewModel Create(LoadEntityListEnum loadEntityType)
			=> new ArticleListViewModel(createLoadCommand: entityListVm => _loadArticlesCommandFactory.Create(entityListVm, loadEntityType));
	}
}
