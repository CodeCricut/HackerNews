using HackerNews.CLI.Application.MediatR.Commands.LogEntityPageWithConfiguration;
using HackerNews.CLI.Application.MediatR.Commands.WriteEntityPageWithConfiguration;
using HackerNews.CLI.Application.MediatR.Queries.GetEntitiesByIds;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.ApplicationRequests.GetEntitiesRequests
{
	public class GetCommentsRequest : IGetEntitiesRequest<GetCommentModel, CommentInclusionConfiguration>
	{
		public CreateVerbosityCommand CreateVerbosityCommand { get; }

		public CreateGetEntitiesByIdsQuery<GetCommentModel> CreateGetEntitiesByIdsQuery { get; }

		public CreateLogEntityPageCommand<GetCommentModel, CommentInclusionConfiguration> CreateLogEntityPageCommand { get; }

		public CreateWriteEntityPageCommand<GetCommentModel, CommentInclusionConfiguration> CreateWriteEntityPageCommand { get; }

		public GetCommentsRequest(GetCommentsOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateGetEntitiesByIdsQuery = () => new GetCommentsByIdsQuery(opts, opts);
			CreateLogEntityPageCommand = commentPage => new LogCommentPageCommand(commentPage, opts, opts.ToInclusionConfiguration());
			CreateWriteEntityPageCommand = commentPage => new WriteCommentPageCommand(commentPage, opts, opts.ToInclusionConfiguration());
		}
	}
}
