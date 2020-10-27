using FluentValidation;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Users.Commands.SaveArticleToUser
{
	class SaveArticleToUserValidator : AbstractValidator<SaveArticleToUserCommand>
	{
		public SaveArticleToUserValidator()
		{
			RuleFor(command => command.ArticleId).SetValidator(new IntIdValidator(), "*");
		}
	}
}
