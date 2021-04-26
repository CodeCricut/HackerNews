using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.WPF.MessageBus.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IBoardHomeViewModelFactory
	{
		//BoardHomeViewModel Create();
		BoardHomeViewModel Create(BoardViewModel boardVm);
	}

	public class BoardHomeViewModelFactory : IBoardHomeViewModelFactory
	{
		private readonly IArticleListViewModelFactory _articleListViewModelFactory;

		public BoardHomeViewModelFactory(
			//IBoardViewModelFactory boardVmFactory,
			IArticleListViewModelFactory articleListViewModelFactory)
		{
			_articleListViewModelFactory = articleListViewModelFactory;
		}

		//public BoardHomeViewModel Create()
		//	=> new BoardHomeViewModel(_boardVmFactory.Create(), _articleListViewModelFactory);

		public BoardHomeViewModel Create(BoardViewModel boardVm)
			=> new BoardHomeViewModel(boardVm, _articleListViewModelFactory);
	}
}
