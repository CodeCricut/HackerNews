using Hackernews.WPF.Core;
using HackerNews.Domain.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Common
{
	public delegate Task<PaginatedList<TGetEntityModel>> LoadPaginatedListByIdsCallback<TGetEntityModel>(List<int> ids, PagingParams pagingParams);

	public class LoadEntityListByIdsCommand<TEntityViewModel, TGetEntityModel> : BaseCommand
		where TEntityViewModel : BaseEntityViewModel
	{
		private readonly EntityListViewModel<TEntityViewModel, TGetEntityModel> _listVM;
		private readonly LoadPaginatedListByIdsCallback<TGetEntityModel> _getEntityPageCallback;
		private readonly ConstructEntityVMCallback<TEntityViewModel, TGetEntityModel> _constructEntityVMCallback;

		public LoadEntityListByIdsCommand(EntityListViewModel<TEntityViewModel, TGetEntityModel> listVM,
			LoadPaginatedListByIdsCallback<TGetEntityModel> getEntityPageCallback,
			ConstructEntityVMCallback<TEntityViewModel, TGetEntityModel> constructEntityVMCallback)
		{
			_listVM = listVM;
			_getEntityPageCallback = getEntityPageCallback;
			_constructEntityVMCallback = constructEntityVMCallback;
		}

		public override async void Execute(object parameter)
		{
			List<int> ids = (List<int>)parameter;
			await App.Current.Dispatcher.Invoke(async () =>
			{
				_listVM.Entities.Clear();

				_listVM.EntityPageVM.Page = await _getEntityPageCallback(ids, _listVM.EntityPageVM.PagingParams);

				foreach (var entity in _listVM.EntityPageVM.Items)
				{
					var entityVm = _constructEntityVMCallback(entity);

					entityVm.LoadEntityCommand.Execute(parameter);

					_listVM.Entities.Add(entityVm);
				}
			});
		}
	}
}
