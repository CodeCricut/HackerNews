using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
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
		private readonly IDeletedEntityPolicyValidator<User> _deletedEntityValidator;

		public GetPublicUserHandler(IDeletedEntityPolicyValidator<User> deletedEntityValidator,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_deletedEntityValidator = deletedEntityValidator;
		}

		public override async Task<GetPublicUserModel> Handle(GetPublicUserQuery request, CancellationToken cancellationToken)
		{
			var user = await UnitOfWork.Users.GetEntityAsync(request.Id);
			if (user == null) throw new NotFoundException();
			user = _deletedEntityValidator.ValidateEntity(user, Domain.Common.DeletedEntityPolicy.OWNER);

			return Mapper.Map<GetPublicUserModel>(user);
		}
	}
}
