using HackerNews.CLI.Requests;
using HackerNews.CLI.Verbs.GetBoardById;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.Configuration
{
	public interface IFileRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		TBaseRequestBuilder BaseRequest { get; }
		string FileLocation { get; set; }
		TBaseRequestBuilder SetWhenBuilt(Func<string> fileLocationCallback);
	}

	public class FileRequestConfiguration<TBaseRequestBuilder, TRequest> : IFileRequestConfiguration<TBaseRequestBuilder, TRequest>
		where TBaseRequestBuilder : IRequestBuilder<TRequest>
	{
		public TBaseRequestBuilder BaseRequest { get; }

		public string FileLocation { get; set; }

		public FileRequestConfiguration(TBaseRequestBuilder requestBuilder)
		{
			BaseRequest = requestBuilder;
		}

		public TBaseRequestBuilder SetWhenBuilt(Func<string> fileLocationCallback)
		{
			BaseRequest.BuildActions.Add(() => FileLocation = fileLocationCallback());
			return BaseRequest;
		}
	}

}
