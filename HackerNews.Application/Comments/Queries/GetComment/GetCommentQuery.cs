using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
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
		public GetCommentHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetCommentModel> Handle(GetCommentQuery request, CancellationToken cancellationToken)
		{
			var comment = await UnitOfWork.Comments.GetEntityAsync(request.Id);
			if (comment == null) throw new NotFoundException();

			return Mapper.Map<GetCommentModel>(comment);
		}
	}
}
