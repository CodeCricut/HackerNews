using AutoMapper;
using HackerNews.Api.Helpers.EntityHelpers;
using HackerNews.Domain;
using HackerNews.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Default
{
	public class DefaultCommentService : CommentService
	{
		public DefaultCommentService(IEntityRepository<Comment> entityRepository, IMapper mapper) : base(entityRepository, mapper)
		{
		}
	}
}
