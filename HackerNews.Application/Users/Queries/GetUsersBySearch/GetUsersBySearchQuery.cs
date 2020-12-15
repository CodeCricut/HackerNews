using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Queries.GetUsersBySearch
{
	public class GetUsersBySearchQuery : IRequest<PaginatedList<GetPublicUserModel>>
	{
		public GetUsersBySearchQuery(string searchTerm, PagingParams pagingParams)
		{
			SearchTerm = searchTerm;
			PagingParams = pagingParams;
		}

		public string SearchTerm { get; }
		public PagingParams PagingParams { get; }
	}

	public class GetUsersBySearchHandler : DatabaseRequestHandler<GetUsersBySearchQuery, PaginatedList<GetPublicUserModel>>
	{
		private readonly IDeletedEntityPolicyValidator<User> _deletedUserValidator;

		public GetUsersBySearchHandler(IDeletedEntityPolicyValidator<User> deletedUserValidator,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_deletedUserValidator = deletedUserValidator;
		}

		public override async Task<PaginatedList<GetPublicUserModel>> Handle(GetUsersBySearchQuery request, CancellationToken cancellationToken)
		{
			var users = await UnitOfWork.Users.GetEntitiesAsync();
			var searchedUsers = users.Where(
				u => u.Username.Contains(request.SearchTerm)
			);

			searchedUsers = _deletedUserValidator.ValidateEntityQuerable(searchedUsers, Domain.Common.DeletedEntityPolicy.OWNER);

			var paginatedSearchedUsers = await PaginatedList<User>.CreateAsync(searchedUsers,
				request.PagingParams.PageNumber, request.PagingParams.PageSize);

			return paginatedSearchedUsers.ToMappedPagedList<User, GetPublicUserModel>(Mapper);
		}
	}
}
