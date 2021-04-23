using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.WPF.MessageBus.Core;
using HackerNews.WPF.MessageBus.ViewModel.EntityHomeWindow;
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

		public ICommand ShowBoardHomeCommand { get; }

		public BoardViewModel(IEventAggregator ea, IApiClient apiClient)
		{
			_ea = ea;
			_apiClient = apiClient;

			LoadEntityCommand = new AsyncDelegateCommand(LoadBoardAsync);

			ShowBoardHomeCommand = new DelegateCommand(ShowBoardHome);
		}

		private void ShowBoardHome(object _ = null) => _ea.SendMessage(new ShowBoardHomeMessage(boardVM: this));

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
