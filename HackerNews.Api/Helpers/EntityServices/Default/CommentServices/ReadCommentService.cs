using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models.Comments;
using HackerNews.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.EntityServices.Base.CommentServices
{
	public class ReadCommentService : ReadEntityService<Comment, GetCommentModel>
	{
		public ReadCommentService(IMapper mapper, IEntityRepository<Comment> entityRepository) : base(mapper, entityRepository)
		{
		}
	}
}
