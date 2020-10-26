using FluentValidation;
using HackerNews.Application.Users.CommonValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Users.Queries.GetUserFromLoginModel
{
	class GetUserFromLoginModelValidator : AbstractValidator<GetUserFromLoginModelQuery>
	{
		public GetUserFromLoginModelValidator()
		{
			RuleFor(query => query.LoginModel).SetValidator(new LoginModelValidator(), "*");
		}
	}
}
