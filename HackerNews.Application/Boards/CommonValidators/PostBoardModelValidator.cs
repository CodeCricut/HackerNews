using FluentValidation;
using HackerNews.Application.Common.Models.Boards;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Boards.CommonValidators
{
	class PostBoardModelValidator : AbstractValidator<PostBoardModel>
	{
		public PostBoardModelValidator()
		{
			RuleFor(model => model).NotNull();
			RuleFor(model => model.Description).NotEmpty();
			RuleFor(model => model.Title).NotEmpty();
		}
	}
}
