using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Queries.GetPublicUser
{
	public class GetPublicUserQuery : IRequest<GetPublicUserModel>
	{
		public GetPublicUserQuery(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}

	public class GetPublicUserHandler : DatabaseRequestHandler<GetPublicUserQuery, GetPublicUserModel>
	{
		public GetPublicUserHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
		}

		public override async Task<GetPublicUserModel> Handle(GetPublicUserQuery request, CancellationToken cancellationToken)
		{
			var user = await UnitOfWork.Users.GetEntityAsync(request.Id);
			if (user == null) throw new NotFoundException();

			return Mapper.Map<GetPublicUserModel>(user);
		}
	}
}
