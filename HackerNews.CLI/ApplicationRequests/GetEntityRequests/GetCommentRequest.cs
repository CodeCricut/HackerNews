using HackerNews.CLI.Application.MediatR.Commands.LogEntityWithConfiguration;
using HackerNews.CLI.Application.MediatR.Commands.WriteEntityWithConfiguration;
using HackerNews.CLI.Application.MediatR.Queries.GetEntityById;
using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.Options.Verbs;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.ApplicationRequests.GetEntityRequests
{
	public class GetCommentRequest : IGetEntityRequest<GetCommentModel, CommentInclusionConfiguration>
	{
		public CreateVerbosityCommand CreateVerbosityCommand { get; }

		public CreateGetEntityQuery<GetCommentModel> CreateGetEntityQuery { get; }

		public CreateLogEntityCommand<GetCommentModel, CommentInclusionConfiguration> CreateLogEntityCommand { get; }

		public CreateWriteEntityCommand<GetCommentModel, CommentInclusionConfiguration> CreateWriteEntityCommand { get; }

		public GetCommentRequest(GetCommentByIdOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateGetEntityQuery = () => new GetCommentByIdQuery(opts);
			CreateLogEntityCommand = article => new LogCommentCommand(article, opts, opts.ToInclusionConfiguration());
			CreateWriteEntityCommand = article => new WriteCommentCommand(article, opts, opts.ToInclusionConfiguration());
		}
	}
}
