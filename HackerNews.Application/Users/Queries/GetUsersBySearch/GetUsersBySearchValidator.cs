using FluentValidation;
using HackerNews.Application.Common.Validators;
using HackerNews.Application.Users.Queries.GetUsersBySearch;

namespace HackerNews.Application.Users.Queries.GetCommentsBySearch
{
	public class GetUsersBySearchValidator : AbstractValidator<GetUsersBySearchQuery>
	{
		public GetUsersBySearchValidator()
		{
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator());
		}
	}
}
