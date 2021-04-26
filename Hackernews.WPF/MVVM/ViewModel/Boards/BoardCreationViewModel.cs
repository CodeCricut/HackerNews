using Hackernews.WPF.Helpers;
using Hackernews.WPF.ViewModels;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.Messages.Application;
using HackerNews.WPF.MessageBus.Messages.ViewModel.EntityCreationWindow;
using System;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Boards
{
	public class BoardCreationViewModel : BaseViewModel
	{
		private PostBoardModel _postBoardModel = new PostBoardModel();
		private readonly IEventAggregator _ea;
		private readonly IBoardApiClient _boardApiClient;

		private PostBoardModel PostBoardModel
		{
			get { return _postBoardModel; }
			set { _postBoardModel = value; RaisePropertyChanged(""); }
		}

		public string Title { get => PostBoardModel.Title; set { PostBoardModel.Title = value; RaisePropertyChanged(); CreateBoardCommand.RaiseCanExecuteChanged(); } }
		public string Description { get => PostBoardModel.Description; set { PostBoardModel.Description = value; RaisePropertyChanged(); CreateBoardCommand.RaiseCanExecuteChanged(); } }


		public AsyncDelegateCommand CreateBoardCommand { get; set; }

		public BoardCreationViewModel(IEventAggregator ea, IBoardApiClient boardApiClient)
		{
			CreateBoardCommand = new AsyncDelegateCommand(CreateBoardAsync, CanCreateBoard);
			_ea = ea;
			_boardApiClient = boardApiClient;
		}

		private async Task CreateBoardAsync(object parameter = null)
		{
			_ea.SendMessage(new EntityCreationWindowLoadingChangedMessage(isLoading: true));
			_ea.SendMessage(new EntityCreationWindowInvalidInputChangedMessage(invalidInput: false));

			try
			{
				var getBoardModel = await _boardApiClient.PostAsync(PostBoardModel);

				_ea.SendMessage(new CloseEntityCreationWindowMessage());
			}
			catch (Exception)
			{
				_ea.SendMessage(new EntityCreationWindowInvalidInputChangedMessage(invalidInput: true));
			}
			finally
			{
				PostBoardModel = new PostBoardModel();
				_ea.SendMessage(new EntityCreationWindowLoadingChangedMessage(isLoading: false));
			}
		}

		private bool CanCreateBoard(object parameter = null) => Title?.Length > 0 && Description?.Length > 0;

	}
}
