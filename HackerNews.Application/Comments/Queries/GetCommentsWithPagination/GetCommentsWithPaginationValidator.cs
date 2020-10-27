using FluentValidation;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Comments.Queries.GetCommentsWithPagination
{
	class GetCommentsWithPaginationValidator : AbstractValidator<GetCommentsWithPaginationQuery>
	{
		public GetCommentsWithPaginationValidator()
		{
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator(), "*");
		}
	}
}
