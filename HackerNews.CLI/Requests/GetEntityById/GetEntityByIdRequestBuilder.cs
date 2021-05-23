using HackerNews.CLI.Verbs.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Requests.GetEntityById
{
	public class GetEntityByIdRequestBuilder<TGetModel, TInclusionConfig> :
		IRequestBuilder<GetEntityByIdRequest<TGetModel, TInclusionConfig>>
	{

		public List<Action> BuildActions { get; }

		public IdRequestConfiguration<
				GetEntityByIdRequestBuilder<TGetModel, TInclusionConfig>,
				GetEntityByIdRequest<TGetModel, TInclusionConfig>> 
			OverrideId { get; }

		public IBoardInclusionRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest> OverrideInclusion { get; }

		public IVerbosityRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest> OverrideVerbosity { get; }

		public IPrintRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest> OverridePrint { get; }

		public IFileRequestConfiguration<GetBoardByIdRequestBuilder, GetBoardByIdRequest> OverrideFile { get; }

		public GetEntityByIdRequest<TGetModel, TInclusionConfig> Build()
		{
			throw new NotImplementedException();
		}
	}
}
