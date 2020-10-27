using FluentValidation;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Boards.Queries.GetBoardsWithPagination
{
	class GetBoardsWithPaginationValidator : AbstractValidator<GetBoardsWithPaginationQuery>
	{
		public GetBoardsWithPaginationValidator()
		{
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator(), "*");
		}
	}
}
