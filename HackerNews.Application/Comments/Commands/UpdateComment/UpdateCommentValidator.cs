﻿using FluentValidation;
using HackerNews.Application.Comments.CommonValidators;
using HackerNews.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Comments.Commands.UpdateComment
{
	class UpdateCommentValidator : AbstractValidator<UpdateCommentCommand>
	{
		public UpdateCommentValidator()
		{
			RuleFor(command => command.CommentId).SetValidator(new IntIdValidator(), "*");
			RuleFor(command => command.PostCommentModel).SetValidator(new PostCommentModelValidator(), "*");
		}
	}
}
