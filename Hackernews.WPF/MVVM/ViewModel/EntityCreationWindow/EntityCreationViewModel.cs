using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.Services;
using HackerNews.WPF.Core.Commands;
using HackerNews.WPF.Core.ViewModel;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.Messages.Application;
using HackerNews.WPF.MessageBus.Messages.ViewModel.EntityCreationWindow;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class EntityCreationViewModel : BaseViewModel
	{
		#region Loading
		private bool _loading;
		public bool Loading
		{
			get { return _loading; }
			set { _loading = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(NotLoading)); }
		}
		public bool NotLoading { get => !Loading; }
		#endregion

		#region User input validation
		private bool _invalidUserInput;
		public bool InvalidUserInput
		{
			get { return _invalidUserInput; }
			set { _invalidUserInput = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(ValidUserInput)); }
		}
		public bool ValidUserInput { get => !InvalidUserInput; }
		#endregion

		#region View switcher
		private object _selectedViewModel;

		public object SelectedViewModel
		{
			get => _selectedViewModel; set
			{
				_selectedViewModel = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(CreationVMIsSelected));
			}
		}

		public bool CreationVMIsSelected => SelectedViewModel != null;

		public ICommand ShowBoardCreationWindowCommand { get; }
		private void ShowBoardCreationWindow(object parameter = null)
		{
			SelectedViewModel = BoardCreationViewModel;
			OpenWindow();
		}

		#endregion

		public BoardCreationViewModel BoardCreationViewModel { get; set; }

		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;

		//public ICommand OpenCommand { get; }
		public ICommand CloseCommand { get; }


		public void OpenWindow() => _viewManager.Show(this);

		public void CloseWindow()
		{
			_ea.UnregisterHandler<CloseEntityCreationWindowMessage>(msg => CloseWindow());
			_ea.UnregisterHandler<EntityCreationWindowLoadingChangedMessage>(msg => Loading = msg.IsLoading);
			_ea.UnregisterHandler<EntityCreationWindowInvalidInputChangedMessage>(msg => InvalidUserInput = msg.InvalidInput);

			_viewManager.Close(this);
		}

		public EntityCreationViewModel(IEventAggregator ea,
		IViewManager viewManager,
		BoardCreationViewModel boardCreationVm
		)
		{
			_ea = ea;
			_viewManager = viewManager;
			BoardCreationViewModel = boardCreationVm;

			SelectedViewModel = BoardCreationViewModel;


			ShowBoardCreationWindowCommand = new DelegateCommand(ShowBoardCreationWindow);
			CloseCommand = new DelegateCommand(_ => CloseWindow());


			ea.RegisterHandler<CloseEntityCreationWindowMessage>(msg => CloseWindow());
			ea.RegisterHandler<EntityCreationWindowLoadingChangedMessage>(msg => Loading = msg.IsLoading);
			ea.RegisterHandler<EntityCreationWindowInvalidInputChangedMessage>(msg => InvalidUserInput = msg.InvalidInput);
		}
	}
}
