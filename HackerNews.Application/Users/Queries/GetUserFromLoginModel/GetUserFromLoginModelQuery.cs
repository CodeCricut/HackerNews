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

namespace HackerNews.Application.Users.Queries.GetUserFromLoginModel
{
	public class GetUserFromLoginModelQuery : IRequest<GetPrivateUserModel>
	{
		public GetUserFromLoginModelQuery(LoginModel loginModel)
		{
			LoginModel = loginModel;
		}

		public LoginModel LoginModel { get; }
	}

	public class GetUserFromLoginModelQueryHandler : DatabaseRequestHandler<GetUserFromLoginModelQuery, GetPrivateUserModel>
	{
		private readonly IDeletedEntityPolicyValidator<User> _deletedUserValidator;

		public GetUserFromLoginModelQueryHandler(IDeletedEntityPolicyValidator<User> deletedUserValidator,
			IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_deletedUserValidator = deletedUserValidator;
		}

		public override async Task<GetPrivateUserModel> Handle(GetUserFromLoginModelQuery request, CancellationToken cancellationToken)
		{
			var users = await UnitOfWork.Users.GetEntitiesAsync();
			var user = users.FirstOrDefault(u => u.Username == request.LoginModel.Username && u.Password == request.LoginModel.Password);
			if (user == null) throw new NotFoundException();

			// Should be able to log into deleted accounts, for now at least
			user = _deletedUserValidator.ValidateEntity(user, Domain.Common.DeletedEntityPolicy.PUBLIC);

			return Mapper.Map<GetPrivateUserModel>(user);
		}
	}
}
