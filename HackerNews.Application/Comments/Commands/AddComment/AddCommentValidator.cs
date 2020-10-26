using FluentValidation;
using HackerNews.Application.Comments.CommonValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Comments.Commands.AddComment
{
	class AddCommentValidator : AbstractValidator<AddCommentCommand>
	{
		public AddCommentValidator()
		{
			RuleFor(command => command.PostCommentModel).SetValidator(new PostCommentModelValidator(), "*");
		}
	}
}
