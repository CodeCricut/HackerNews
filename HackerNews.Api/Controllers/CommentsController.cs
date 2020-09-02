using AutoMapper;
using HackerNews.Domain.Models;
using HackerNews.EF;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Domain;
using Microsoft.AspNetCore.Http;
using HackerNews.Api.Profiles;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class CommentsController : ControllerBase
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IArticleRepository _articleRepository;
		private readonly IMapper _mapper;

		public CommentsController(ICommentRepository commentRepository, 
								IArticleRepository articleRepository, 
								IMapper mapper)
		{
			_commentRepository = commentRepository;
			_articleRepository = articleRepository;
			_mapper = mapper;
		}

		// add include children bool param
		public async Task<IActionResult> Get()
		{
			try
			{
				// results in circular dependency
				//var comments = await _artCommRepository.GetCommentsWithParentsAsync(includeChildren: false);

				var comments = (await _commentRepository.GetCommentsAsync(true)).ToList();

				// in order to avoid altering the actual references (such as children comments), we must deep clone the list
				var cloneComments = comments.ConvertAll(c => new Comment(c));

				//for(int i = 0; i < comments.Count(); i++)
				//{
				//	var comment = comments[i];
				//	comment = EntityTrimmer.GetNewTrimmedComment(comment, trimParents: false, trimChildren: false);
				//}

				for (int i = 0; i < cloneComments.Count(); i++)
				{
					var comment = cloneComments[i];
					comment = EntityTrimmer.GetNewTrimmedComment(comment, trimParents: false, trimChildren: false);
				}

				// TODO: not always mapping parents to IDs
				var models = _mapper.Map<IEnumerable<GetCommentModel>>(cloneComments);
				
				return Ok(models);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				//var comment = await _artCommRepository.GetCommentWithParentAsync(id, includeChildren: true);

				var comment = await _commentRepository.GetCommentAsync(id, true);
				comment = EntityTrimmer.GetNewTrimmedComment(comment, false, false);
				var model = _mapper.Map<GetCommentModel>(comment);
				
				return Ok(model);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		public async Task<IActionResult> PostComment([FromBody] PostCommentModel commentModel)
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				Comment comment = _mapper.Map<Comment>(commentModel);

				Article parentArticle = null;
				Comment parentComment = null;
				
				
				// if there is a parent comment given
				if (commentModel.ParentCommentId >= 1)
				{
					parentComment = await AddChildToComment(commentModel.ParentCommentId, comment);
				}
				if (commentModel.ParentArticleId >= 1)
				{
					parentArticle = await AddChildToArticle(commentModel.ParentArticleId, comment);
				}


				_commentRepository.AddComment(comment);

				await _commentRepository.SaveChangesAsync();
				await _articleRepository.SaveChangesAsync();
				
				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		private async Task<Article> AddChildToArticle(int articleId, Comment childComment)
		{
			Article parentArticle =
				await _articleRepository.GetArticleAsync(articleId);
				//await _artCommRepository.GetArticleWithChildrenAsync(articleId);
			
			// add parent to child
			childComment.ParentArticle = parentArticle;

			await _commentRepository.SaveChangesAsync();

			// add child to parent
			//parentArticle.Comments.Add(childComment);
			//_articleRepository.UpdateArticle(parentArticle.Id, parentArticle);


			parentArticle = await _articleRepository.GetArticleAsync(articleId);
			return parentArticle;
		}

		private async Task<Comment> AddChildToComment(int parentId, Comment childComment)
		{
			Comment parentComment =
				await _commentRepository.GetCommentAsync(parentId, true);
				// await _artCommRepository.GetCommentWithParentAsync(parentId, includeChildren: true);

			// add the parent reference to the child
			childComment.ParentComment = parentComment;

			// add the child reference to the parent
			//parentComment.Comments.Add(childComment);
			//_commentRepository.UpdateComment(childComment.Id, childComment);

			await _commentRepository.SaveChangesAsync();

			parentComment = await _commentRepository.GetCommentAsync(parentId, true);

			return parentComment;
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutComment(int id, [FromBody] PostCommentModel commentModel)
		{
			try
			{
				if (!ModelState.IsValid) throw new Exception("Model invalid");

				var comment =
					await _commentRepository.GetCommentAsync(id, true);
					//await _artCommRepository.GetCommentWithParentAsync(id, includeChildren: true);
				
				// this is messy, but a quick fix
				comment.AuthorName = commentModel.AuthorName;
				comment.Text = commentModel.Text;

				// not gonna support rebasing comments...
				// comment.ParentComment = parentComment;
			    // comment.ParentArticle = parentArticle;

				_commentRepository.UpdateComment(id, comment);
				await _commentRepository.SaveChangesAsync();

				comment =
					await _commentRepository.GetCommentAsync(id, true);
					//await _artCommRepository.GetCommentWithParentAsync(id, includeChildren: true);
				return Ok(comment);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteArticle(int id)
		{
			try
			{
				_commentRepository.DeleteComment(id);
				await _commentRepository.SaveChangesAsync();
				
				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost("vote/{commentId:int}")]
		public async Task<IActionResult> VoteArticle(int commentId, [FromBody] bool upvote)
		{
			try
			{
				var comment =
					await _commentRepository.GetCommentAsync(commentId, true);
					//await _artCommRepository.GetCommentWithParentAsync(commentId, includeChildren: true);

				if (upvote)
					comment.Karma++;
				else comment.Karma--;
				
				_commentRepository.UpdateComment(commentId, comment);
				await _commentRepository.SaveChangesAsync();
				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
