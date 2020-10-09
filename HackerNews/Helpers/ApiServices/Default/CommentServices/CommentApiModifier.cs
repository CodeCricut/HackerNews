using HackerNews.Domain;
using HackerNews.Domain.Models.Comments;
using HackerNews.Helpers.ApiServices.Base;
using HackerNews.Helpers.Cookies.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Default.CommentServices
{
	public class CommentApiModifier : ApiModifier<Comment, PostCommentModel, GetCommentModel>
	{
		public CommentApiModifier(IHttpClientFactory clientFactory, IOptions<AppSettings> options, IJwtService jwtService) : base(clientFactory, options, jwtService)
		{
		}
	}
}
