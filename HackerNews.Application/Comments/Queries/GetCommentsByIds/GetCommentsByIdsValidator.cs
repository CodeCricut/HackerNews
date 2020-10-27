using FluentValidation;
using HackerNews.Application.Common.Validators;

namespace HackerNews.Application.Comments.Queries.GetCommentsByIds
{
	class GetCommentsByIdsValidator : AbstractValidator<GetCommentsByIdsQuery>
	{
		public GetCommentsByIdsValidator()
		{
			RuleForEach(query => query.Ids).SetValidator(new IntIdValidator(), "*");
			RuleFor(query => query.PagingParams).SetValidator(new PagingParamsValidator(), "*");
		}
	}
}
