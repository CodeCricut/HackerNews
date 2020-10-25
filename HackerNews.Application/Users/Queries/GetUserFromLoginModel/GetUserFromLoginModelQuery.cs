using AutoMapper;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Errors;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		public GetUserFromLoginModelQueryHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<GetPrivateUserModel> Handle(GetUserFromLoginModelQuery request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var users = await UnitOfWork.Users.GetEntitiesAsync();
				var user = users.FirstOrDefault(u => u.Username == request.LoginModel.Username && u.Password == request.LoginModel.Password);
				if (user == null) throw new NotFoundException();

				return Mapper.Map<GetPrivateUserModel>(user);
			}
		}
	}
}
