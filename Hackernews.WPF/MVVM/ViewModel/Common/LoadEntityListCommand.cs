using Hackernews.WPF.Core;
using HackerNews.Domain.Common.Models;
using System;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Common
{
	public delegate Task<PaginatedList<TGetEntityModel>> LoadPaginatedListCallback<TGetEntityModel>(PagingParams pagingParams);
	public delegate TEntityViewModel ConstructEntityVMCallback<TEntityViewModel, TGetEntityModel>(TGetEntityModel entityModel);

	public class LoadEntityListCommand<TEntityViewModel, TGetEntityModel> : BaseCommand
		where TEntityViewModel : BaseEntityViewModel
	{
		private readonly EntityListViewModel<TEntityViewModel, TGetEntityModel> _listVM;
		private readonly LoadPaginatedListCallback<TGetEntityModel> _getEntityPageCallback;
		private readonly ConstructEntityVMCallback<TEntityViewModel, TGetEntityModel> _constructEntityVMCallback;

		public LoadEntityListCommand(EntityListViewModel<TEntityViewModel, TGetEntityModel> listVM,
			LoadPaginatedListCallback<TGetEntityModel> getEntityPageCallback,
			ConstructEntityVMCallback<TEntityViewModel, TGetEntityModel> constructEntityVMCallback)
		{
			_listVM = listVM;
			_getEntityPageCallback = getEntityPageCallback;
			_constructEntityVMCallback = constructEntityVMCallback;
		}

		public override async void Execute(object parameter)
		{
			await App.Current.Dispatcher.Invoke(async () =>
			{
				_listVM.Entities.Clear();
				_listVM.EntityPageVM.Page = await _getEntityPageCallback(_listVM.EntityPageVM.PagingParams);

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
