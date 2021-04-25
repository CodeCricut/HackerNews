using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Comments;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Comments;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Articles
{
	public class ArticleHomeViewModel : BaseViewModel
	{
		public EntityListViewModel<CommentViewModel, GetCommentModel> ArticleCommentsListVM { get; }

		public ArticleViewModel ArticleViewModel { get; }

		public AsyncDelegateCommand LoadArticleCommand { get; }

		public ArticleHomeViewModel(ArticleViewModel articleVm, IApiClient apiClient)
		{
			ArticleViewModel = articleVm;

			ArticleCommentsListVM = new CommentListViewModel(createLoadCommand: commentListVm => new LoadCommentsByIdsCommand(commentListVm, apiClient));

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
