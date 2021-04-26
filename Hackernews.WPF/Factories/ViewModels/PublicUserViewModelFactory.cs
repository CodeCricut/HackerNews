using Hackernews.WPF.MVVM.ViewModel;
using HackerNews.ApiConsumer.Images;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories.ViewModels
{
	public interface IPublicUserViewModelFactory
	{
		PublicUserViewModel Create();
	}

	public class PublicUserViewModelFactory : IPublicUserViewModelFactory
	{
		private readonly IImageApiClient _imageApiClient;

		public PublicUserViewModelFactory(IImageApiClient imageApiClient)
		{
			_imageApiClient = imageApiClient;
		}

		public PublicUserViewModel Create()
			=> new PublicUserViewModel(_imageApiClient);
	}
}
