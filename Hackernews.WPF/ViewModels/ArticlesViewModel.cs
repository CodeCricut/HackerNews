using HackerNews.Domain.Common.Models.Articles;
using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hackernews.WPF.ViewModels
{
	public class ArticlesViewModel : BaseViewModel
	{
		public ObservableCollection<GetArticleModel> Articles { get; private set; }

		private GetArticleModel _article;
		public GetArticleModel Article { 
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

		public ArticlesViewModel(GetArticleModel article)
		{
			Article = article;
			Articles = new ObservableCollection<GetArticleModel>();
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

		public void LoadArticles()
		{
			var articles = new List<GetArticleModel>()
			{
					new GetArticleModel
				{
					Title = "title 1",
					Text = "text 1",
					Karma = 69,
					PostDate = DateTime.Now,
					Type = HackerNews.Domain.Entities.ArticleType.News,
					Deleted = true
				},

				new GetArticleModel
				{
					Title = "title 2",
					Text = "text 2",
					Karma = 420,
					PostDate = DateTime.Now,
					Type = HackerNews.Domain.Entities.ArticleType.Opinion,
					Deleted = false
				}
			};

			foreach (var article in articles)
			{
				Articles.Add(article);
			}
		}
	}
}
