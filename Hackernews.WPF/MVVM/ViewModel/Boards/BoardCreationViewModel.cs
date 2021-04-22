using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.ViewModels;
using HackerNews.Domain.Common.Models.Boards;
using System;
using System.Threading.Tasks;

namespace Hackernews.WPF.MVVM.ViewModel.Boards
{
	public class BoardCreationViewModel : BaseViewModel
	{
		private PostBoardModel _postBoardModel = new PostBoardModel();
		private readonly EntityCreationViewModel _entityCreationViewModel;
		private readonly IApiClient _apiClient;

		private PostBoardModel PostBoardModel
		{
			get { return _postBoardModel; }
			set { _postBoardModel = value; RaisePropertyChanged(""); }
		}

		public string Title { get => PostBoardModel.Title; set { PostBoardModel.Title = value; RaisePropertyChanged(); CreateBoardCommand.RaiseCanExecuteChanged(); } }
		public string Description { get => PostBoardModel.Description; set { PostBoardModel.Description = value; RaisePropertyChanged(); CreateBoardCommand.RaiseCanExecuteChanged(); } }

		public AsyncDelegateCommand CreateBoardCommand { get; set; }

		public BoardCreationViewModel(EntityCreationViewModel entityCreationViewModel, IApiClient apiClient)
		{

			CreateBoardCommand = new AsyncDelegateCommand(CreateBoardAsync, CanCreateBoard);
			_entityCreationViewModel = entityCreationViewModel;
			_apiClient = apiClient;
		}

		private async Task CreateBoardAsync(object parameter = null)
		{
			_entityCreationViewModel.Loading = true;
			_entityCreationViewModel.InvalidUserInput = false;

			try
			{
				var getBoardModel = await _apiClient.PostAsync<PostBoardModel, GetBoardModel>(PostBoardModel, "boards");

				await Task.Factory.StartNew(() => _entityCreationViewModel.CloseCommand.Execute(parameter));
			}
			catch (Exception)
			{
				_entityCreationViewModel.InvalidUserInput = true;
			}
			finally
			{
				PostBoardModel = new PostBoardModel();
				_entityCreationViewModel.Loading = false;
			}
		}

		private bool CanCreateBoard(object parameter = null) => Title?.Length > 0 && Description?.Length > 0;

	}
}
