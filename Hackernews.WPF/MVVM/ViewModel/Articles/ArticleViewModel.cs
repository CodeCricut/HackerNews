using Hackernews.WPF.ApiClients;
using Hackernews.WPF.Services;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Entities;
using System;
using System.ComponentModel;
using System.Linq;

namespace Hackernews.WPF.ViewModels
{
	public class ArticleViewModel : BaseViewModel
	{
		private readonly PrivateUserViewModel _privateUserVM;

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


		public ArticleViewModel(PrivateUserViewModel privateUser)
		{
			_privateUserVM = privateUser;
			_privateUserVM.PropertyChanged += new PropertyChangedEventHandler((obj, target) => RaiseUserCreatedArticleChanged());
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
