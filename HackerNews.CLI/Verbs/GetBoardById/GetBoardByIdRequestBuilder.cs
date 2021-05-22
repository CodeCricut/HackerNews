using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Verbs.GetBoardById
{
	public class GetBoardByIdRequestBuilder
	{

		// ===== IBoardInclusionRequestConfiguration ===== //
		private BoardInclusionConfiguration _boardInclusionConfiguration;

		public GetBoardByIdRequestBuilder ConfigureBoardInclusion(IBoardInclusionOptions boardInclusionOpts
			)
		{
			_boardInclusionConfiguration = boardInclusionOpts.GetInclusionConfiguration();
			return this;
		}

		// ===== IVerbosityRequestConfiguration ===== //
		private bool _verbosity;
		public GetBoardByIdRequestBuilder ConfigureVerbosity(IVerbosityOptions verbosityOptions)
		{
			_verbosity = verbosityOptions.Verbose;
			return this;
		}

		public GetBoardByIdRequestBuilder ConfigureVerbosity(Func<bool> verboseCallback)
		{
			// TODO; only invoke callback in Build();
			_verbosity = verboseCallback();
			return this;
		}

		// ===== IPrintRequestConfiguration ===== //
		private bool _print;
		public GetBoardByIdRequestBuilder ConfigurePrint(IPrintOptions printOptions)
		{
			_print = printOptions.Print;
			return this;
		}

		// ===== IFileRequestConfiguration ===== //
		private string _fileLocation;
		public GetBoardByIdRequestBuilder ConfigureFile(IFileOptions fileOptions)
		{
			_fileLocation = fileOptions.FileLocation;
			return this;
		}

		// ===== IIdConfiguration ===== //
		private int _id;

		public GetBoardByIdRequestBuilder ConfigureId(IIdOptions idOptions)
		{
			_id = idOptions.Id;
			return this;
		}

		public GetBoardByIdRequestBuilder Configure(GetBoardByIdOptions options
			)
		{
			ConfigureVerbosity(options)
			.ConfigureBoardInclusion(options)
			.ConfigurePrint(options)
			.ConfigureFile(options)
			.ConfigureId(options);
			
			return this;
		}

		private readonly GetBoardByIdRequestFactory _requestFactory;
		public GetBoardByIdRequestBuilder(GetBoardByIdRequestFactory requestFactory)
		{
			_requestFactory = requestFactory;
		}

		public GetBoardByIdRequest Build()
		{
			// TODO: execute configuration actions of provided
			return _requestFactory.Create(
				_boardInclusionConfiguration,
				_verbosity,
				_print,
				_fileLocation,
				_id);
		}
	}
}
