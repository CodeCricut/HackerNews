using FluentValidation;

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
