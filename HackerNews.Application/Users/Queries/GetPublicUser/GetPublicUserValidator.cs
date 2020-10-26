using FluentValidation;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Users.Queries.GetPublicUser
{
	class GetPublicUserValidator : AbstractValidator<GetPublicUserQuery>
	{
		public GetPublicUserValidator()
		{
			RuleFor(query => query.Id).SetValidator(new IntIdValidator(), "*");
		}
	}
}
