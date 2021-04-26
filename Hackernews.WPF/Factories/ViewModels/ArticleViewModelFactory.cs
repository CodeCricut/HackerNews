using Hackernews.WPF.MVVM.ViewModel;
using Hackernews.WPF.Services;
using Hackernews.WPF.ViewModels;
using HackerNews.ApiConsumer.Images;
using HackerNews.WPF.MessageBus.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IArticleViewModelFactory
	{
		ArticleViewModel Create();
		ArticleViewModel Create(PrivateUserViewModel userVm);
	}

	public class ArticleViewModelFactory : IArticleViewModelFactory
	{
		private readonly PrivateUserViewModel _userVm;
		private readonly IImageApiClient _imageApiClient;
		private readonly IEntityHomeViewModelFactory _entityHomeViewModelFactory;
		private readonly IArticleHomeViewModelFactory _articleHomeViewModelFactory;

		public ArticleViewModelFactory(
			PrivateUserViewModel userVm,
			IImageApiClient imageApiClient,
			IEntityHomeViewModelFactory entityHomeViewModelFactory,
			IArticleHomeViewModelFactory articleHomeViewModelFactory)
		{
			_userVm = userVm;
			_imageApiClient = imageApiClient;
			_entityHomeViewModelFactory = entityHomeViewModelFactory;
			_articleHomeViewModelFactory = articleHomeViewModelFactory;
		}

		public ArticleViewModel Create()
			=> new ArticleViewModel(articleViewModelFactory: this, _articleHomeViewModelFactory, _imageApiClient, _entityHomeViewModelFactory, _userVm);

		public ArticleViewModel Create(PrivateUserViewModel userVm)
			=> new ArticleViewModel(articleViewModelFactory: this, _articleHomeViewModelFactory, _imageApiClient, _entityHomeViewModelFactory, userVm);

	}
}
