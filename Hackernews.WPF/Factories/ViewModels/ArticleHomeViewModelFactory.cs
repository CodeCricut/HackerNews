using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Articles;
using HackerNews.ApiConsumer.EntityClients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IArticleHomeViewModelFactory
	{
		ArticleHomeViewModel Create(ArticleViewModel articleVm);
	}

	public class ArticleHomeViewModelFactory : IArticleHomeViewModelFactory
	{
		private readonly IArticleApiClient _articleApiClient;
		private readonly ICommentListViewModelFactory _commentListViewModelFactory;

		public ArticleHomeViewModelFactory(
			IArticleApiClient articleApiClient,
			ICommentListViewModelFactory commentListViewModelFactory)
		{
			_articleApiClient = articleApiClient;
			_commentListViewModelFactory = commentListViewModelFactory;
		}

		public ArticleHomeViewModel Create(ArticleViewModel articleVm)
			=> new ArticleHomeViewModel(articleVm, _articleApiClient, _commentListViewModelFactory);
	}
}
