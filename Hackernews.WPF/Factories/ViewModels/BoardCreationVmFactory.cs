//using Hackernews.WPF.ApiClients;
//using Hackernews.WPF.MVVM.ViewModel;
//using Hackernews.WPF.MVVM.ViewModel.Boards;

//namespace Hackernews.WPF.Factories.ViewModels
//{
//	public interface IBoardCreationVmFactory
//	{
//		BoardCreationViewModel Create(EntityCreationViewModel parentEntityCreationViewModel);
//	}

//	public class BoardCreationVmFactory : IBoardCreationVmFactory
//	{
//		private readonly IApiClient _apiClient;

//		public BoardCreationVmFactory(IApiClient apiClient)
//		{
//			_apiClient = apiClient;
//		}

//		public BoardCreationViewModel Create(EntityCreationViewModel parentEntityCreationViewModel) => new BoardCreationViewModel(parentEntityCreationViewModel, _apiClient);
//	}
//}
