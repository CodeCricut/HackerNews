using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.Behaviors
{
	public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			// Inject all validators that apply to the request
			_validators = validators;
		}

		public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			var validationFailures = _validators
				.Select(validator => validator.Validate(request))
				.SelectMany(validationResult => validationResult.Errors)
				.Where(validationFailure => validationFailure != null)
				.ToList();

			if (validationFailures.Any())
			{
				throw new ValidationException(validationFailures);
			}

			return next();
		}
	}
}
