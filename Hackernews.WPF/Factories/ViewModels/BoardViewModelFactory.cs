using Hackernews.WPF.MVVM.ViewModel;
using HackerNews.ApiConsumer.Images;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IBoardViewModelFactory
	{
		BoardViewModel Create();
	}

	public class BoardViewModelFactory : IBoardViewModelFactory
	{
		private readonly IBoardHomeViewModelFactory _boardHomeViewModelFactory;
		private readonly IEntityHomeViewModelFactory _entityHomeViewModelFactory;
		private readonly IImageApiClient _imageApiClient;

		public BoardViewModelFactory(
			IBoardHomeViewModelFactory boardHomeViewModelFactory,
			IEntityHomeViewModelFactory entityHomeViewModelFactory,
			IImageApiClient imageApiClient)
		{
			_boardHomeViewModelFactory = boardHomeViewModelFactory;
			_entityHomeViewModelFactory = entityHomeViewModelFactory;
			_imageApiClient = imageApiClient;
		}

		public BoardViewModel Create()
			=> new BoardViewModel(boardViewModelFactory: this, _boardHomeViewModelFactory, _entityHomeViewModelFactory, _imageApiClient);
	}
}
