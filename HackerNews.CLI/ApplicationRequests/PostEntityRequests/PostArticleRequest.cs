using HackerNews.CLI.Application.MediatR.Commands.LogEntity;
using HackerNews.CLI.Application.MediatR.Commands.PostEntity;
using HackerNews.CLI.Application.MediatR.Commands.WriteEntity;
using HackerNews.CLI.Domain.Verbs;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.MediatR.Commands.SignIn;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.ApplicationRequests.PostEntityRequests
{
	public class PostArticleRequest : IPostEntityRequest<PostArticleModel, GetArticleModel>
	{
		public CreateVerbosityCommand CreateVerbosityCommand { get; }
		public CreateSignInCommand CreateSignInCommand { get; }
		public CreatePostCommand<PostArticleModel, GetArticleModel> CreatePostCommand { get; }
		public CreateLogEntityCommand<GetArticleModel> CreateLogCommand { get; }

		public CreateWriteEntityCommand<GetArticleModel> CreateWriteCommand { get; }

		public PostArticleRequest(PostArticleOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateSignInCommand = () => new SignInCommand(opts);
			CreatePostCommand = () => new PostArticleCommand(opts.ToPostModel());
			CreateLogCommand = postedArticle => new LogArticleCommand(postedArticle, opts);
			CreateWriteCommand = postedArticle => new WriteArticleCommand(postedArticle, opts);
		}
	}
}
