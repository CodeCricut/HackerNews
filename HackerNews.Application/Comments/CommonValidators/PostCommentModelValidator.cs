﻿using FluentValidation;
using HackerNews.Application.Common.Models.Comments;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Comments.CommonValidators
{
	class PostCommentModelValidator : AbstractValidator<PostCommentModel>
	{
		public PostCommentModelValidator()
		{
			RuleFor(model => model.ParentArticleId).SetValidator(new IntIdValidator(), "*");
			RuleFor(model => model.ParentCommentId).SetValidator(new IntIdValidator(), "*");
			RuleFor(model => model.Text).NotEmpty();
		}
	}
}
