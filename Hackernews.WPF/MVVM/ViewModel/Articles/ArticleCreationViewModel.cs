using Hackernews.WPF.Services;
using HackerNews.ApiConsumer.EntityClients;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.MessageBus.Core;
using HackerNews.WPF.Core.Commands;
using HackerNews.WPF.Core.Services;
using HackerNews.WPF.Core.ViewModel;
using HackerNews.WPF.MessageBus.Messages.ViewModel.EntityCreationWindow;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.MVVM.ViewModel.Articles
{
	public class ArticleCreationViewModel : BaseViewModel
	{
		private PostArticleModel _postArticleModel = new PostArticleModel();
		private readonly IEventAggregator _ea;
		private readonly IViewManager _viewManager;
		private readonly IArticleApiClient _articleApiClient;

		private PostArticleModel PostArticleModel
		{
			get { return _postArticleModel; }
			set { _postArticleModel = value; RaisePropertyChanged(""); }
		}

		public ObservableCollection<string> TestComboboxSource { get; } = new ObservableCollection<string>()
		{
			"News", "Opinion", "Question", "Meta"
		};

		public string Title { get => PostArticleModel.Title; set { PostArticleModel.Title = value; RaisePropertyChanged(); CreateArticleCommand.RaiseCanExecuteChanged(); } }

		public string Text { get => PostArticleModel.Text; set { PostArticleModel.Text = value; RaisePropertyChanged(); CreateArticleCommand.RaiseCanExecuteChanged(); } }

		public string Type { get => PostArticleModel.Type; set { PostArticleModel.Type = value; RaisePropertyChanged(); CreateArticleCommand.RaiseCanExecuteChanged(); } }

		public AsyncDelegateCommand CreateArticleCommand { get; }
		private bool CanCreateArticle(object parameter = null)
			=> Title?.Length > 0 && Text?.Length > 0 && Type?.Length > 0 && PostArticleModel.BoardId > 0;

		public void OpenWindow() => _viewManager.Show(this);

		public void CloseWindow() => _viewManager.Close(this);

		public ICommand OpenCommand { get; }
		public ICommand CloseCommand { get; }

		public ArticleCreationViewModel(GetBoardModel parentBoard,
			IEventAggregator ea,
			IViewManager viewManager,
			IArticleApiClient articleApiClient)
		{
			_ea = ea;
			_viewManager = viewManager;
			_articleApiClient = articleApiClient;

			PostArticleModel.BoardId = parentBoard.Id;

			CreateArticleCommand = new AsyncDelegateCommand(CreateArticleAsync, CanCreateArticle);
			OpenCommand = new DelegateCommand(_ => OpenWindow());
			CloseCommand = new DelegateCommand(_ => CloseWindow());
		}

		private async Task CreateArticleAsync(object _ = null)
		{
			_ea.SendMessage(new EntityCreationWindowLoadingChangedMessage(isLoading: true));
			_ea.SendMessage(new EntityCreationWindowInvalidInputChangedMessage(invalidInput: false));

			try
			{
				var getArticleModel = await _articleApiClient.PostAsync(PostArticleModel);

				CloseWindow();

				// TODO: send reload articles message
			}
			catch (Exception)
			{
				_ea.SendMessage(new EntityCreationWindowInvalidInputChangedMessage(invalidInput: true));
			}
			finally
			{
				PostArticleModel = new PostArticleModel();
				_ea.SendMessage(new EntityCreationWindowLoadingChangedMessage(isLoading: false));
			}
		}
	}
}
