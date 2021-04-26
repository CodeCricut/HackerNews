using Hackernews.WPF.Factories.ViewModels;
using Hackernews.WPF.Helpers;
using Hackernews.WPF.MVVM.ViewModel.Articles;
using Hackernews.WPF.MVVM.ViewModel.Common;
using Hackernews.WPF.ViewModels;
using HackerNews.ApiConsumer.Images;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Common.Models.Images;
using HackerNews.Domain.Entities;
using HackerNews.WPF.Core.Commands;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class ArticleViewModel : BaseEntityViewModel
	{
		private readonly PrivateUserViewModel _privateUserVM;
		private readonly IArticleViewModelFactory _articleViewModelFactory;
		private readonly IArticleHomeViewModelFactory _articleHomeViewModelFactory;
		private readonly IImageApiClient _imageApiClient;
		private readonly IEntityHomeViewModelFactory _entityHomeViewModelFactory;

		private GetArticleModel _article;
		public GetArticleModel Article
		{
			get => _article;
			set
			{
				Set(ref _article, value);
				RaisePropertyChanged(string.Empty); // update all props
			}
		}

		private bool _isSelected;
		public bool IsSelected
		{
			get { return _isSelected; }
			set { _isSelected = value; RaisePropertyChanged(); }
		}

		public ICommand ShowArticleHomeCommand { get; }

		public ArticleViewModel(
			IArticleViewModelFactory articleViewModelFactory,
			IArticleHomeViewModelFactory articleHomeViewModelFactory,
			IImageApiClient imageApiClient,
			IEntityHomeViewModelFactory entityHomeViewModelFactory,
			PrivateUserViewModel privateUser
			)
		{
			_privateUserVM = privateUser;
			_articleViewModelFactory = articleViewModelFactory;
			_articleHomeViewModelFactory = articleHomeViewModelFactory;
			_imageApiClient = imageApiClient;
			_entityHomeViewModelFactory = entityHomeViewModelFactory;

			// TODO: go through ea.
			_privateUserVM.PropertyChanged += new PropertyChangedEventHandler((obj, target) => RaiseUserCreatedArticleChanged());

			LoadEntityCommand = new AsyncDelegateCommand(LoadArticleAsync);

			ShowArticleHomeCommand = new DelegateCommand(ShowArticleHome);
		}

		private void ShowArticleHome(object _ = null)
		{
			EntityHomeViewModel entityHome = _entityHomeViewModelFactory.Create();

			// Copy the article vm reference to keep it always selected.
			var newArticleVm = _articleViewModelFactory.Create();
			newArticleVm.Article = this.Article;
			newArticleVm.IsSelected = true;
			newArticleVm.LoadEntityCommand.Execute();

			ArticleHomeViewModel articleHomeVm = _articleHomeViewModelFactory.Create(newArticleVm);
			articleHomeVm.LoadArticleCommand.Execute();

			entityHome.ShowArticleHome(articleHomeVm);
		}

		private BitmapImage _articleImage;

		public BitmapImage ArticleImage
		{
			get { return _articleImage; }
			set { _articleImage = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(HasImage)); RaisePropertyChanged(nameof(HasNoImage)); }
		}

		public bool HasImage { get => ArticleImage != null; }
		public bool HasNoImage { get => ArticleImage == null; }

		private async Task LoadArticleAsync(object parameter = null)
		{
			if (Article?.AssociatedImageId > 0)
			{
				GetImageModel imgModel = await _imageApiClient.GetImageAsync(Article.AssociatedImageId);
				BitmapImage bitmapImg = BitmapUtil.LoadImage(imgModel.ImageData);
				ArticleImage = bitmapImg;
			}
		}


		public void RaiseUserCreatedArticleChanged() => RaisePropertyChanged(nameof(UserCreatedArticle));

		public bool UserCreatedArticle
		{
			get => _article != null && _privateUserVM.ArticleIds.Contains(_article.Id);
		}

		public string Title
		{
			get => _article?.Title ?? "";
			set
			{
				if (_article.Title != value)
				{
					_article.Title = value;
					RaisePropertyChanged();
				}
			}
		}

		public string Text
		{
			get => _article?.Text ?? "";
			set
			{
				if (_article.Text != value)
				{
					_article.Text = value;
					RaisePropertyChanged();
				}
			}
		}

		public int Karma
		{
			get => _article?.Karma ?? 0;
			set
			{
				if (_article.Karma != value)
				{
					_article.Karma = value;
					RaisePropertyChanged();
				}
			}
		}

		public bool IsDeleted
		{
			get => _article?.Deleted ?? false;
			set
			{
				if (_article.Deleted != value)
				{
					_article.Deleted = value;
					RaisePropertyChanged();
				}
			}
		}

		public DateTime PostDate
		{
			get => _article?.PostDate ?? new DateTime(0);
			set
			{
				if (_article.PostDate != value)
				{
					_article.PostDate = value;
					RaisePropertyChanged();
					RaisePropertyChanged(nameof(PostDateOffset));
				}
			}
		}

		public DateTimeOffset PostDateOffset
		{
			get => _article?.PostDate ?? new DateTime(0);
			set
			{
				if (!_article.PostDate.Equals(value.DateTime))
				{
					_article.PostDate = value.DateTime;
					RaisePropertyChanged();
					RaisePropertyChanged(nameof(PostDate));
				}
			}
		}

		public string TypeName
		{
			get
			{
				if (_article == null) return "";
				return _article.Type switch
				{
					ArticleType.Meta => "Meta",
					ArticleType.News => "News",
					ArticleType.Opinion => "Opinion",
					_ => "Question",
				};
			}
			set
			{
				if (!TypeName.Equals(value))
				{
					_article.Type = value switch
					{
						"Meta" => ArticleType.Meta,
						"News" => ArticleType.News,
						"Opinion" => ArticleType.Opinion,
						_ => ArticleType.Question
					};

					RaisePropertyChanged();
				}
			}
		}
	}
}
