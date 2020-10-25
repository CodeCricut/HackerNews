using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Comments.Queries.GetComment
{
	public class GetCommentQuery : IRequest<GetCommentModel>
	{
		public GetCommentQuery(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}

	public class GetCommentHandler : DatabaseRequestHandler<GetCommentQuery, GetCommentModel>
	{
		public GetCommentHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<GetCommentModel> Handle(GetCommentQuery request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var comment = await UnitOfWork.Comments.GetEntityAsync(request.Id);
				if (comment == null) throw new NotFoundException();

				return Mapper.Map<GetCommentModel>(comment);
			}
		}
	}
}
