using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models.Articles;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class ArticleListViewModel : EntityListViewModel<ArticleViewModel, GetArticleModel>
	{
		public ArticleListViewModel(Core.CreateBaseCommand<EntityListViewModel<ArticleViewModel, GetArticleModel>> createLoadCommand) : base(createLoadCommand)
		{
		}
	}
}
