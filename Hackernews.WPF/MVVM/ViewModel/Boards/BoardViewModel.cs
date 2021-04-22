using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Images;
using System;
using System.Threading.Tasks;
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
		private readonly IApiClient _apiClient;

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

<<<<<<< HEAD
=======
		public AsyncDelegateCommand LoadEntityCommand { get; }

>>>>>>> summaries
		public BoardViewModel(IApiClient apiClient, PrivateUserViewModel userVm)
		{
			LoadEntityCommand = new AsyncDelegateCommand(LoadBoardAsync);
			_apiClient = apiClient;

			EntityHomeViewModel = new EntityHomeViewModel(this, apiClient, userVm);
		}

		public EntityHomeViewModel EntityHomeViewModel { get;  }

		private BitmapImage _boardImage;

		public BitmapImage BoardImage
		{
			get { return _boardImage; }
			set { _boardImage = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(HasImage)); RaisePropertyChanged(nameof(HasNoImage)); }
		}

		public bool HasImage { get => BoardImage != null; }
		public bool HasNoImage { get => BoardImage == null;  }


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
