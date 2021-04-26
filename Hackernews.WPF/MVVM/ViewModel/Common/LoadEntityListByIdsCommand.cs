using HackerNews.Domain.Common.Models;
using HackerNews.WPF.Core.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Common
{
	public abstract class LoadEntityListByIdsCommand<TEntityViewModel, TGetEntityModel> : BaseCommand
		where TEntityViewModel : BaseEntityViewModel
	{
		private readonly EntityListViewModel<TEntityViewModel, TGetEntityModel> _listVM;

		public LoadEntityListByIdsCommand(EntityListViewModel<TEntityViewModel, TGetEntityModel> listVM)
		{
			_listVM = listVM;
		}

		public override async void Execute(object parameter)
		{
			List<int> ids = (List<int>)parameter;
			await App.Current.Dispatcher.Invoke(async () =>
			{
				_listVM.Entities.Clear();

				_listVM.EntityPageVM.Page = await LoadEntityModelsAsync(ids, _listVM.EntityPageVM.PagingParams);

				foreach (var entity in _listVM.EntityPageVM.Items)
				{
					var entityVm = ConstructEntityViewModel(entity);

					entityVm.LoadEntityCommand.Execute(parameter);

					_listVM.Entities.Add(entityVm);
				}

				_listVM.EntityPageVM.PrevPageCommand.RaiseCanExecuteChanged();
				_listVM.EntityPageVM.NextPageCommand.RaiseCanExecuteChanged();
			});
		}

		public abstract Task<PaginatedList<TGetEntityModel>> LoadEntityModelsAsync(List<int> ids, PagingParams pagingParams);

		public abstract TEntityViewModel ConstructEntityViewModel(TGetEntityModel getModel);
	}
}
