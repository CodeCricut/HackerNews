using Hackernews.WPF.Factories.Commands;
using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.MVVM.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface ICommentListViewModelFactory
	{
		CommentListViewModel Create(LoadEntityListEnum loadEntityType);
	}

	public class CommentListViewModelFactory : ICommentListViewModelFactory
	{
		private readonly ILoadCommentsCommandFactory _loadCommentsCommandFactory;

		public CommentListViewModelFactory(ILoadCommentsCommandFactory loadCommentsCommandFactory)
		{
			_loadCommentsCommandFactory = loadCommentsCommandFactory;
		}

		public CommentListViewModel Create(LoadEntityListEnum loadEntityType)
			=> new CommentListViewModel(createLoadCommand: commentListVm => _loadCommentsCommandFactory.Create(commentListVm, loadEntityType));
	}
}
