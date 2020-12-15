using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Queries.GetUserByUsername
{
	public class GetUserByUsernameQuery : IRequest<GetPublicUserModel>
	{
		public GetUserByUsernameQuery(string username)
		{
			Username = username;
		}

		public string Username { get; }
	}

	public class GetUserByUsernameHandler : DatabaseRequestHandler<GetUserByUsernameQuery, GetPublicUserModel>
	{
		private readonly IDeletedEntityPolicyValidator<User> _deletedEntityValidator;

		public GetUserByUsernameHandler(IDeletedEntityPolicyValidator<User> deletedEntityValidator,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_deletedEntityValidator = deletedEntityValidator;
		}

		public override async Task<GetPublicUserModel> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
		{
			var users = await UnitOfWork.Users.GetEntitiesAsync();

			var user = users.FirstOrDefault(u => u.Username == request.Username);

			if (user == null || user.Id <= 0) throw new NotFoundException();

			user = _deletedEntityValidator.ValidateEntity(user, Domain.Common.DeletedEntityPolicy.OWNER);

			return Mapper.Map<GetPublicUserModel>(user);
		}
	}
}
