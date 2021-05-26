using HackerNews.ApiConsumer.Core;
using HackerNews.CLI.MediatR.Commands.PostEntity;
using HackerNews.Domain.Common.Models.Articles;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.CLI.Application.MediatR.Commands.PostEntity
{
	public class PostArticleCommand : PostEntityCommand<PostArticleModel, GetArticleModel>
	{
		public PostArticleCommand(PostArticleModel postModel) : base(postModel)
		{
		}
	}

	public class PostArticleCommandHandler : PostEntityCommandHandler<PostArticleCommand, PostArticleModel, GetArticleModel>
	{
		public PostArticleCommandHandler(IEntityApiClient<PostArticleModel, GetArticleModel> entityApiClient) : base(entityApiClient)
		{
		}
	}
}
