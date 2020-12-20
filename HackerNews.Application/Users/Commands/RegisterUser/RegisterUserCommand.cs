using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Commands.RegisterUser
{
	public class RegisterUserCommand : IRequest<User>
	{
		public RegisterUserCommand(RegisterUserModel registerUserModel)
		{
			RegisterUserModel = registerUserModel;
		}

		public RegisterUserModel RegisterUserModel { get; }
	}

	public class RegisterUserHandler : DatabaseRequestHandler<RegisterUserCommand, User>
	{
		private readonly UserManager<User> _userManager;

		public RegisterUserHandler(UserManager<User> userManager, IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(unitOfWork, mediator, mapper, currentUserService)
		{
			_userManager = userManager;
		}

		public override async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			// Map user
			var user = Mapper.Map<User>(request.RegisterUserModel);
			user.JoinDate = DateTime.Now;

			// Verify username isn't taken
			var users = await UnitOfWork.Users.GetEntitiesAsync();
			var userWithUsername = users.FirstOrDefault(u => u.UserName == user.UserName);
			if (userWithUsername != null) throw new UsernameTakenException();

			// Create user	
			var result = await _userManager.CreateAsync(user, request.RegisterUserModel.Password);

			if (!result.Succeeded)
				throw new InvalidPostException("There was an error registering.");

			// Just in case
			UnitOfWork.SaveChanges();

			return user;
		}
	}
}
