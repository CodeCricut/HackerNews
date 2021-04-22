using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Comments;
using System;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class CommentViewModel : BaseEntityViewModel
	{
		private bool _isSelected;

		public bool IsSelected
		{
			get { return _isSelected; }
			set { _isSelected = value; RaisePropertyChanged(); }
		}


		private GetCommentModel _comment;
		public GetCommentModel Comment
		{
			get => _comment;
			set
			{
				if (_comment != value)
				{
					_comment = value;
					RaisePropertyChanged("");
				}
			}
		}

		public string Text
		{
			get => _comment?.Text ?? "";
			set
			{
				if (_comment.Text != value)
				{
					_comment.Text = value;
					RaisePropertyChanged();
				}
			}
		}

		public int Karma
		{
			get => _comment?.Karma ?? 0;
			set
			{
				if (_comment.Karma != value)
				{
					_comment.Karma = value;
					RaisePropertyChanged();
				}
			}
		}

		public DateTime PostDate
		{
			get => _comment?.PostDate ?? new DateTime(0);
			set
			{
				if (_comment.PostDate != value)
				{
					_comment.PostDate = value;
					RaisePropertyChanged();
					RaisePropertyChanged(nameof(PostDateOffset));
				}
			}
		}

		public DateTimeOffset PostDateOffset
		{
			get => new DateTimeOffset(PostDate);
			set
			{
				PostDate = value.Date;
			}
		}
	}
}
