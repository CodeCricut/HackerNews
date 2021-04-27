using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.Services;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.WPF.MessageBus.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IBoardCreationViewModelFactory
	{
		BoardCreationViewModel Create();
	}

	public class BoardCreationViewModelFactory : IBoardCreationViewModelFactory
	{
		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;
		private readonly IBoardApiClient _boardApiClient;

		public BoardCreationViewModelFactory(IEventAggregator ea,
			IViewManager viewManager,
			IBoardApiClient boardApiClient)
		{
			_ea = ea;
			_viewManager = viewManager;
			_boardApiClient = boardApiClient;
		}

		public BoardCreationViewModel Create()
			=> new BoardCreationViewModel(_ea, _viewManager, _boardApiClient);
	}
}
