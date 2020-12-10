using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Users.Queries.GetUserByUsername
{
	public class GetUserByUsernameValidator : AbstractValidator<GetUserByUsernameQuery>
	{
		public GetUserByUsernameValidator()
		{
			RuleFor(query => query.Username).NotEmpty();
		}
	}
}
