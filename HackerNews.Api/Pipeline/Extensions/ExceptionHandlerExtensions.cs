using HackerNews.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HackerNews.Api.Pipeline.Extensions
{
	public static class ExceptionHandlerExtensions
	{
		/// <summary>
		/// Returns a JSON <see cref="ErrorResponse"/> based on any uncaught exception.
		/// </summary>
		/// <param name="app"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder app)
		{
			return app.UseExceptionHandler(
				a => a.Run(async context =>
				{
					var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();
					var exception = exceptionHandlerPathFeature.Error; // Your exception
					var code = 500; // Internal Server Error by default

					if (exception is BoardTitleTakenException ||
						exception is UsernameTakenException ||
						exception is InvalidPostException) code = 400; // Bad Request
					else if (exception is EntityDeletedException ||
							 exception is NotFoundException) code = 404; // Not Found
					else if (exception is UnauthorizedException) code = 401; // Unauthorized

					var result = JsonConvert.SerializeObject(new ErrorResponse(exception));

					context.Response.StatusCode = code;
					context.Response.ContentType = "application/json";
					await context.Response.WriteAsync(result);
				}));
		}
	}
}
