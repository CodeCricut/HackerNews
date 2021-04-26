using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Boards;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.ApiConsumer.Core;
using HackerNews.ApiConsumer.Images;
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
		private readonly IBoardViewModelFactory _boardViewModelFactory;
		private readonly IBoardHomeViewModelFactory _boardHomeViewModelFactory;
		private readonly IEntityHomeViewModelFactory _entityHomeViewModelFactory;
		private readonly IImageApiClient _imageApiClient;

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

		public BoardViewModel(
			IBoardViewModelFactory boardViewModelFactory,
			IBoardHomeViewModelFactory boardHomeViewModelFactory,
			IEntityHomeViewModelFactory entityHomeViewModelFactory,
			IImageApiClient imageApiClient)
		{
			_boardViewModelFactory = boardViewModelFactory;
			_boardHomeViewModelFactory = boardHomeViewModelFactory;
			_entityHomeViewModelFactory = entityHomeViewModelFactory;
			_imageApiClient = imageApiClient;
			LoadEntityCommand = new AsyncDelegateCommand(LoadBoardAsync);

			ShowBoardHomeCommand = new DelegateCommand(ShowBoardHome);
		}

		private void ShowBoardHome(object _ = null)
		{
			EntityHomeViewModel boardHomeVm = _entityHomeViewModelFactory.Create();

			// Copy the board vm to keep it always selected
			var newBoardVm = _boardViewModelFactory.Create();
			newBoardVm.Board = this.Board;
			newBoardVm.IsSelected = true;

			BoardHomeViewModel boardHomeVM = _boardHomeViewModelFactory.Create(newBoardVm);
			boardHomeVM.LoadBoardCommand.Execute();

			boardHomeVm.ShowBoardHome(boardHomeVM);
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
				GetImageModel imgModel = await _imageApiClient.GetImageAsync(Board.BoardImageId);
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
