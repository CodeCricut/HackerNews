using Hackernews.WPF.Helpers;
using HackerNews.Application.Articles.Queries.GetArticle;
using HackerNews.Application.Articles.Queries.GetArticlesWithPagination;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hackernews.WPF.ViewModels
{
	public class ArticlesViewModel : BaseViewModel
	{
		public ICommand LoadCommand { get; }

		public ObservableCollection<GetArticleModel> Articles { get; private set; }
		
		public GetArticleModel Article
		{
			get => _article;
			set
			{
				if (_article != value)
				{
					_article = value;
					RaisePropertyChanged();
					RaisePropertyChanged(string.Empty); // update all props
				}
			}
		}

		private GetArticleModel _article;
		private readonly IMediator _mediator;

		public ArticlesViewModel(GetArticleModel article, IMediator mediator)
		{
			Article = article;
			_mediator = mediator;
			Articles = new ObservableCollection<GetArticleModel>();

			LoadCommand = new AsyncDelegateCommand(LoadArticlesAsync);
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
				if (! _article.PostDate.Equals(value.DateTime))
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

		public bool IsArticleSelected { get => Article != null; }

		public async Task LoadArticlesAsync()
		{
			Articles.Clear();

			var articlePage = await _mediator.Send(new GetArticlesWithPaginationQuery(new PagingParams()));
			foreach (var article in articlePage.Items)
			{
				Articles.Add(article);
			}
		}
	}
}
