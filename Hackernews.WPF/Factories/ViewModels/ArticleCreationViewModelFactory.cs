using Hackernews.WPF.MVVM.ViewModel.Articles;
using Hackernews.WPF.Services;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.WPF.Core.Services;
using HackerNews.WPF.MessageBus.Core;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IArticleCreationViewModelFactory
	{
		ArticleCreationViewModel Create(GetBoardModel parentBoard);
	}

	public class ArticleCreationViewModelFactory : IArticleCreationViewModelFactory
	{
		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;
		private readonly IArticleApiClient _articleApiClient;

		public ArticleCreationViewModelFactory(IEventAggregator ea,
			IViewManager viewManager,
			IArticleApiClient articleApiClient)
		{
			_ea = ea;
			_viewManager = viewManager;
			_articleApiClient = articleApiClient;
		}

		public ArticleCreationViewModel Create(GetBoardModel parentBoard)
			=> new ArticleCreationViewModel(parentBoard, _ea, _viewManager, _articleApiClient);
	}
}
