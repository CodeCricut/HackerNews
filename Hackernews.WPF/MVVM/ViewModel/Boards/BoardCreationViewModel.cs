using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.Messages.Application;
using Hackernews.WPF.Messages.ViewModel.EntityCreationWindow;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.WPF.MessageBus.Core;
using System;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Boards
{
	public class BoardCreationViewModel : BaseViewModel
	{
		private PostBoardModel _postBoardModel = new PostBoardModel();
		private readonly IEventAggregator _ea;
		private readonly IApiClient _apiClient;

		private PostBoardModel PostBoardModel
		{
			get { return _postBoardModel; }
			set { _postBoardModel = value; RaisePropertyChanged(""); }
		}

		public string Title { get => PostBoardModel.Title; set { PostBoardModel.Title = value; RaisePropertyChanged(); CreateBoardCommand.RaiseCanExecuteChanged(); } }
		public string Description { get => PostBoardModel.Description; set { PostBoardModel.Description = value; RaisePropertyChanged(); CreateBoardCommand.RaiseCanExecuteChanged(); } }


		public AsyncDelegateCommand CreateBoardCommand { get; set; }

		public BoardCreationViewModel(IEventAggregator ea, IApiClient apiClient)
		{
			CreateBoardCommand = new AsyncDelegateCommand(CreateBoardAsync, CanCreateBoard);
			_ea = ea;
			_apiClient = apiClient;
		}

		private async Task CreateBoardAsync(object parameter = null)
		{
			_ea.SendMessage(new EntityCreationWindowLoadingChangedMessage(isLoading: true));
			_ea.SendMessage(new EntityCreationWindowInvalidInputChangedMessage(invalidInput: false));

			try
			{
				var getBoardModel = await _apiClient.PostAsync<PostBoardModel, GetBoardModel>(PostBoardModel, "boards");

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
