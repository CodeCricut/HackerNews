﻿using Hackernews.WPF.MVVM.Model;
using HackerNews.WPF.Core.Commands;
using HackerNews.WPF.Core.ViewModel;
using System.Collections.ObjectModel;

namespace Hackernews.WPF.MVVM.ViewModel.Common
{
	public class EntityListViewModel<TEntityViewModel, TGetEntityModel> : BaseViewModel
	{
		public ObservableCollection<TEntityViewModel> Entities { get; private set; } = new ObservableCollection<TEntityViewModel>();

		public PaginatedListViewModel<TGetEntityModel> EntityPageVM { get; }

		public BaseCommand LoadCommand { get; }

		public EntityListViewModel(CreateBaseCommand<EntityListViewModel<TEntityViewModel, TGetEntityModel>> createLoadCommand)
		{
			LoadCommand = createLoadCommand(this);

			EntityPageVM = new PaginatedListViewModel<TGetEntityModel>(LoadCommand);
		}
	}
}
