using Hackernews.WPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface ICommentViewModelFactory
	{
		CommentViewModel Create();
	}

	public class CommentViewModelFactory : ICommentViewModelFactory
	{
		public CommentViewModel Create()
			=> new CommentViewModel();
	}
}
