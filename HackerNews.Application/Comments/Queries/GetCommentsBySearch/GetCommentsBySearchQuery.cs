using AutoMapper;
using HackerNews.Application.Common.DeletedEntityValidators;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Comments;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Comments.Queries.GetCommentsBySearch
{
	public class GetCommentsBySearchQuery : IRequest<PaginatedList<GetCommentModel>>
	{
		public GetCommentsBySearchQuery(string searchTerm, PagingParams pagingParams)
		{
			SearchTerm = searchTerm;
			PagingParams = pagingParams;
		}

		public string SearchTerm { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetCommentsBySearchHandler : DatabaseRequestHandler<GetCommentsBySearchQuery, PaginatedList<GetCommentModel>>
	{
		private readonly IDeletedEntityPolicyValidator<Comment> _deletedCommentValidator;

		public GetCommentsBySearchHandler(IDeletedEntityPolicyValidator<Comment> deletedCommentValidator,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_deletedCommentValidator = deletedCommentValidator;
		}

		public override async Task<PaginatedList<GetCommentModel>> Handle(GetCommentsBySearchQuery request, CancellationToken cancellationToken)
		{
			var comments = await UnitOfWork.Comments.GetEntitiesAsync();
			var searchedComments = comments.Where(c =>
				c.Text.Contains(request.SearchTerm)
			);

			searchedComments = _deletedCommentValidator.ValidateEntityQuerable(searchedComments, Domain.Common.DeletedEntityPolicy.OWNER);

			var paginatedSearchedComments = await PaginatedList<Comment>.CreateAsync(searchedComments,
				request.PagingParams.PageNumber, request.PagingParams.PageSize);

			return paginatedSearchedComments.ToMappedPagedList<Comment, GetCommentModel>(Mapper);
		}
	}
}
