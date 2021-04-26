using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.ApiConsumer.Core;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.WPF.MessageBus.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class BoardViewModel : BaseEntityViewModel
	{
		private bool _isSelected;

		public bool IsSelected
		{
			get { return _isSelected; }
			set { _isSelected = value; RaisePropertyChanged(); }
		}

		private GetBoardModel _board;
		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;
		private readonly IApiClient _apiClient;
		private readonly PrivateUserViewModel _userVm;

		public GetBoardModel Board
		{
			get => _board;
			set
			{
				if (_board != value)
				{
					_board = value;
					RaisePropertyChanged("");
				}
			}
		}

		public ICommand ShowBoardHomeCommand { get; }

		public BoardViewModel(IEventAggregator ea,
			IViewManager viewManager,
			IApiClient apiClient, PrivateUserViewModel userVm)
		{
			_ea = ea;
			_viewManager = viewManager;
			_apiClient = apiClient;
			_userVm = userVm;
			LoadEntityCommand = new AsyncDelegateCommand(LoadBoardAsync);

			ShowBoardHomeCommand = new DelegateCommand(ShowBoardHome);
		}

		private void ShowBoardHome(object _ = null)
		{
			// todo: make factory
			EntityHomeViewModel boardHomeVm = new EntityHomeViewModel(_ea, _viewManager, _apiClient, _userVm);
			boardHomeVm.ShowBoardHome(this);
		}

		private BitmapImage _boardImage;

		public BitmapImage BoardImage
		{
			get { return _boardImage; }
			set { _boardImage = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(HasImage)); RaisePropertyChanged(nameof(HasNoImage)); }
		}

		public bool HasImage { get => BoardImage != null; }
		public bool HasNoImage { get => BoardImage == null; }


		private async Task LoadBoardAsync(object parameter = null)
		{
			if (Board?.BoardImageId > 0)
			{
				GetImageModel imgModel = await _apiClient.GetAsync<GetImageModel>(Board.BoardImageId, "images");
				BitmapImage bitmapImg = BitmapUtil.LoadImage(imgModel.ImageData);
				BoardImage = bitmapImg;
			}
		}

		public string Title
		{
			get => _board?.Title ?? "";
			set
			{
				if (_board.Title != value)
				{
					_board.Title = value;
					RaisePropertyChanged();
				}
			}
		}

		public string Description
		{
			get => _board?.Description ?? "";
			set
			{
				if (_board.Description != value)
				{
					_board.Description = value;
					RaisePropertyChanged();
				}
			}
		}

		public DateTime CreateDate
		{
			get => _board?.CreateDate ?? new DateTime(0);
			set
			{
				if (_board.CreateDate != value)
				{
					_board.CreateDate = value;
					RaisePropertyChanged();
					RaisePropertyChanged(nameof(CreateDateOffset));
				}
			}
		}

		public DateTimeOffset CreateDateOffset
		{
			get => new DateTimeOffset(CreateDate);
			set
			{
				CreateDate = value.Date;
			}
		}
	}
}
