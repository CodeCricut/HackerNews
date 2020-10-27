using FluentValidation;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Articles.Queries.GetArticlesWithPagination
{
	class GetArticlesWithPaginationValidator : AbstractValidator<GetArticlesWithPaginationQuery>
	{
		public GetArticlesWithPaginationValidator()
		{
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator(), "*");
		}
	}
}
