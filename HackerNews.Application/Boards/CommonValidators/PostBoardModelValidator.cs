using FluentValidation;
using HackerNews.Application.Common.Models.Boards;

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
