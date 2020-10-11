using AutoMapper;
using CleanEntityArchitecture.EntityModelServices;
using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using HackerNews.Domain.Models.Comments;

namespace HackerNews.Api.Helpers.EntityServices.Base.CommentServices
{
	public class ReadCommentService : ReadEntityService<Comment, GetCommentModel>
	{
		public ReadCommentService(IMapper mapper, IReadEntityRepository<Comment> readRepository) : base(mapper, readRepository)
		{
		}
	}
}
