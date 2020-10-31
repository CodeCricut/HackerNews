using FluentValidation;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.Application.Boards.CommonValidators
{
	class PostBoardModelValidator : AbstractValidator<PostBoardModel>
	{
		public PostBoardModelValidator()
		{
			RuleFor(model => model).NotNull();
			RuleFor(model => model.Description).NotEmpty();
			RuleFor(model => model.Title).NotEmpty();
		}
	}
}
