using HackerNews.Domain.Common.Models.Boards;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.ViewModels
{
	public class BoardViewModel : BaseViewModel
	{
		private GetBoardModel _board;

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

		public string Title
		{
			get => _board.Title;
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
			get => _board.Description; 
			set {
				if (_board.Description != value)
				{
					_board.Description = value;
					RaisePropertyChanged();
				}
			}
		}

		public DateTime CreateDate
		{
			get => _board.CreateDate;
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
