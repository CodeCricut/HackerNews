using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.WPF.Core.Commands;
using HackerNews.WPF.Core.ViewModel;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Articles
{
	public class ArticleHomeViewModel : BaseViewModel
	{
		private readonly IArticleApiClient _articleApiClient;

		public EntityListViewModel<CommentViewModel, GetCommentModel> ArticleCommentsListVM { get; }

		public ArticleViewModel ArticleViewModel { get; }

		public AsyncDelegateCommand LoadArticleCommand { get; }

		public ArticleHomeViewModel(ArticleViewModel articleVm, IArticleApiClient articleApiClient,
			ICommentListViewModelFactory commentListViewModelFactory
			)
		{
			ArticleViewModel = articleVm;
			_articleApiClient = articleApiClient;
			ArticleCommentsListVM = commentListViewModelFactory.Create(LoadEntityListEnum.LoadByIds);

			LoadArticleCommand = new AsyncDelegateCommand(LoadArticle);
		}

		private Task LoadArticle(object _ = null)
		{
			return Task.Factory.StartNew(() =>
			{
				LoadArticleComments();
				ArticleViewModel.LoadEntityCommand.Execute(_);
			});
		}

		private void LoadArticleComments() => ArticleCommentsListVM.LoadCommand.Execute(ArticleViewModel.Article.CommentIds);
	}
}
