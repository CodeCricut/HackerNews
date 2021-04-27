using Hackernews.WPF.Services;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.WPF.Core.Commands;
using HackerNews.WPF.Core.ViewModel;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.Messages.Application;
using HackerNews.WPF.MessageBus.Messages.ViewModel.EntityCreationWindow;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel.Boards
{
	public class BoardCreationViewModel : BaseViewModel
	{
		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;
		private readonly IBoardApiClient _boardApiClient;

		#region Loading Props
		private bool _loading;
		public bool Loading
		{
			get { return _loading; }
			set { _loading = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(NotLoading)); }
		}
		public bool NotLoading { get => !Loading; }
		#endregion

		#region User input validation props
		private bool _invalidUserInput;
		public bool InvalidUserInput
		{
			get { return _invalidUserInput; }
			set { _invalidUserInput = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(ValidUserInput)); }
		}
		public bool ValidUserInput { get => !InvalidUserInput; }
		#endregion


		private PostBoardModel _postBoardModel = new PostBoardModel();
		private PostBoardModel PostBoardModel
		{
			get { return _postBoardModel; }
			set { _postBoardModel = value; RaisePropertyChanged(""); }
		}

		public string Title { get => PostBoardModel.Title; set { PostBoardModel.Title = value; RaisePropertyChanged(); CreateBoardCommand.RaiseCanExecuteChanged(); } }
		public string Description { get => PostBoardModel.Description; set { PostBoardModel.Description = value; RaisePropertyChanged(); CreateBoardCommand.RaiseCanExecuteChanged(); } }

		public AsyncDelegateCommand CreateBoardCommand { get; set; }
		private bool CanCreateBoard(object parameter = null) => Title?.Length > 0 && Description?.Length > 0;

		public void OpenWindow() => _viewManager.Show(this);

		public void CloseWindow() => _viewManager.Close(this);

		public ICommand OpenCommand { get; }
		public ICommand CloseCommand { get; }

		public BoardCreationViewModel(IEventAggregator ea, 
			IViewManager viewManager,
			IBoardApiClient boardApiClient)
		{
			_ea = ea;
			_viewManager = viewManager;
			_boardApiClient = boardApiClient;
			
			CreateBoardCommand = new AsyncDelegateCommand(CreateBoardAsync, CanCreateBoard);
			OpenCommand = new DelegateCommand(_ => OpenWindow());
			CloseCommand = new DelegateCommand(_ => CloseWindow());
		}

		private async Task CreateBoardAsync(object parameter = null)
		{
			Loading = true;
			InvalidUserInput = false;

			try
			{
				var getBoardModel = await _boardApiClient.PostAsync(PostBoardModel);

				_ea.SendMessage(new CloseEntityCreationWindowMessage());
			}
			catch (Exception)
			{
				InvalidUserInput = true;
			}
			finally
			{
				PostBoardModel = new PostBoardModel();
				Loading = false;
			}
		}


	}
}
