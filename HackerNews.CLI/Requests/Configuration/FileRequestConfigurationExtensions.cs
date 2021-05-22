﻿using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.GetEntity;

namespace HackerNews.CLI.Verbs.Configuration
{
	public static class FileRequestConfigurationExtensions
	{
		public static TBaseRequestBuilder FromOptions<TBaseRequestBuilder, TRequest>(
			this IFileRequestConfiguration<TBaseRequestBuilder, TRequest> fileConfiguration,
			IFileOptions options)
			where TBaseRequestBuilder : IRequestBuilder<TRequest>
		{
			fileConfiguration.FileLocation = options.FileLocation;
			return fileConfiguration.BaseRequest;
		}
	}
}