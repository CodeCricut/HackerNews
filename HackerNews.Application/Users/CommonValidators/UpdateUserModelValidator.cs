﻿using FluentValidation;
using HackerNews.Domain.Common.Models.Users;

namespace HackerNews.Application.Users.CommonValidators
{
	class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
	{
		public UpdateUserModelValidator()
		{
			RuleFor(model => model.FirstName).NotEmpty();
			RuleFor(model => model.LastName).NotEmpty();
		}
	}
}
