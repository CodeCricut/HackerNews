using FluentValidation;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Users.Queries.GetPublicUsersWithPagination
{
	class GetPublicUsersWithPaginationValidator : AbstractValidator<GetPublicUsersWithPaginationQuery>
	{
		public GetPublicUsersWithPaginationValidator()
		{
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator(), "*");
		}
	}
}
