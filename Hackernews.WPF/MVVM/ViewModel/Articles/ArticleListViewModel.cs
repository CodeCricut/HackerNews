using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.WPF.Core.Commands;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class ArticleListViewModel : EntityListViewModel<ArticleViewModel, GetArticleModel>
	{
		public ArticleListViewModel(CreateBaseCommand<EntityListViewModel<ArticleViewModel, GetArticleModel>> createLoadCommand) : base(createLoadCommand)
		{
		}
	}
}
