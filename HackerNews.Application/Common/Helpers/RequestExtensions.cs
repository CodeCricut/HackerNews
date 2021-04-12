using MediatR;
using System;
using System.Threading.Tasks;

namespace HackerNews.Application.Common.Helpers
{
	public static class RequestExtensions
	{
		/// <summary>
		/// Send the <paramref name="request"/> to <paramref name="mediator"/> and return the response if successful. If an exception is throw,
		/// the default <typeparamref name="TResponse"/> will be returned.
		/// </summary>
		public static async Task<TResponse> DefaultIfExceptionAsync<TException, TResponse>(
			this IRequest<TResponse> request,
			IMediator mediator
			)
			where TException : Exception
		{
			try
			{
				return await mediator.Send(request);
			}
			catch (TException)
			{
				return default(TResponse);
			}
		}

		/// <summary>
		/// Send the <paramref name="request"/> to <paramref name="mediator"/> and return the response if successful. If an exception is throw,
		/// the default <typeparamref name="TResponse"/> will be returned.
		/// </summary>
		public static async Task<TResponse> DefaultIfExceptionAsync<TResponse>(
			this IRequest<TResponse> request,
			IMediator mediator
			)
		{
			try
			{
				return await mediator.Send(request);
			}
			catch (Exception)
			{
				return default(TResponse);
			}
		}
	}
}
