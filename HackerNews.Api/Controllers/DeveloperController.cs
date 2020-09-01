﻿using HackerNews.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class DeveloperController : ControllerBase
	{
		private readonly IArticleRepository _articleRepository;
		private readonly ICommentRepository _commentRepository;
		private readonly HackerNewsContext _context;

		public DeveloperController(IArticleRepository articleRepository, ICommentRepository commentRepository, HackerNewsContext context)
		{
			_articleRepository = articleRepository;
			_commentRepository = commentRepository;
			_context = context;
		}

		[HttpOptions]
		public async Task<IActionResult> DeleteAllData()
		{
			try
			{
				var articles = await _articleRepository.GetArticlesWithoutChildrenAsync();
				var comments = await _commentRepository.GetCommentsWithoutParentAsync(true);

				_context.RemoveRange(articles);
				_context.RemoveRange(comments);

				_context.SaveChanges();

				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			
		}
	}
}
