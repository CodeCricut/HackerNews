using HackerNews.CLI.Application.MediatR.Commands.LogEntity;
using HackerNews.CLI.Application.MediatR.Commands.PostEntity;
using HackerNews.CLI.Application.MediatR.Commands.WriteEntity;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.MediatR.Commands.SignIn;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.ApplicationRequests.PostEntityRequests
{
	public class PostCommentRequest : IPostEntityRequest<PostCommentModel, GetCommentModel>
	{
		public CreateVerbosityCommand CreateVerbosityCommand { get; }
		public CreateSignInCommand CreateSignInCommand { get; }
		public CreatePostCommand<PostCommentModel, GetCommentModel> CreatePostCommand { get; }
		public CreateLogEntityCommand<GetCommentModel> CreateLogCommand { get; }

		public CreateWriteEntityCommand<GetCommentModel> CreateWriteCommand { get; }

		public PostCommentRequest(PostCommentOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateSignInCommand = () => new SignInCommand(opts);
			CreatePostCommand = () => new PostCommentCommand(opts.ToPostModel());
			CreateLogCommand = postedComment => new LogCommentCommand(postedComment, opts);
			CreateWriteCommand = postedComment => new WriteCommentCommand(postedComment, opts);
		}
	}
}
