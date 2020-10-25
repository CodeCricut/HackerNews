using HackerNews.Api.Controllers.Base;
using HackerNews.Api.Controllers.Interfaces;
using HackerNews.Application.Articles.Commands.AddArticle;
using HackerNews.Application.Articles.Commands.AddArticles;
using HackerNews.Application.Articles.Commands.AddComment;
using HackerNews.Application.Common.Models.Articles;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers.ArticleControllers
{
	[Route("api/Articles")]
	public class WriteArticleController : ApiController, IWriteEntityController<Article, PostArticleModel, GetArticleModel>, IWriteArticleController
	{
		public async Task<ActionResult<GetArticleModel>> AddComment(int articleId, PostCommentModel postCommentModel)
		{
			return Ok(await Mediator.Send(new AddArticleCommentCommand(articleId, postCommentModel)));
		}

		public async Task<ActionResult> Delete(int key)
		{
			throw new NotImplementedException();
			// todo
		}

		public async Task<ActionResult<GetArticleModel>> PostAsync([FromBody] PostArticleModel postModel)
		{
			return Ok(await Mediator.Send(new AddArticleCommand(postModel)));
		}

		public async Task<ActionResult> PostRangeAsync([FromBody] IEnumerable<PostArticleModel> postModels)
		{
			return Ok(await Mediator.Send(new AddArticlesCommand(postModels)));
		}

		public async Task<ActionResult<GetArticleModel>> Put(int key, [FromBody] PostArticleModel updateModel)
		{
			throw new System.NotImplementedException();
			// todo
		}
	}
}
