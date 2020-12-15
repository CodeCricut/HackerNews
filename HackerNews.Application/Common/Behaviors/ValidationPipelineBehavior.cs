using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.Behaviors
{
	/// <summary>
	/// Runs all <typeparamref name="TRequest"/>s through all <see cref="IValidator{TRequest}"/>.
	/// </summary>
	/// <typeparam name="TRequest"></typeparam>
	/// <typeparam name="TResponse"></typeparam>
	public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			// Inject all validators that apply to the request
			_validators = validators;
		}

		/// <summary>
		/// Run the request through the validators. If failures, throw <seealso cref="ValidationException"/>. Else, run the request as normal.
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="next"></param>
		/// <returns></returns>
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
