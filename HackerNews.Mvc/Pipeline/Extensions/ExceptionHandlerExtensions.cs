using HackerNews.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace HackerNews.Mvc.Pipeline.Extensions
{
	public static class ExceptionHandlerExtensions
	{
		/// <summary>
		/// Redirects to an appropriate page when applicable. 
		/// </summary>
		/// <param name="app"></param>
		/// <returns></returns>
		public static IApplicationBuilder UseMvcExceptionHandler(this IApplicationBuilder app)
		{
			return app.UseExceptionHandler(
				a => a.Run(async context =>
				{
					var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();
					var exception = exceptionHandlerPathFeature.Error; // Your exception
					var code = 500; // Internal Server Error by default

					await context.Response.WriteAsync($"<html lang=\"en\"><body>\r\n");

					if (exception is BoardTitleTakenException ||
						exception is UsernameTakenException ||
						exception is InvalidPostException) // Bad Request
					{
						code = 400;
						await context.Response.WriteAsync($"Invalid request. This can happen for a variety of reasons.");
					}
					else if (exception is EntityDeletedException ||
							 exception is NotFoundException)  // Not Found
					{
						code = 404;
						await context.Response.WriteAsync($"The requested resource could not be found. This could be because it is deleted or never existed.");
					}
					else if (exception is UnauthorizedException)  // Unauthorized
					{
						code = 401;
						await context.Response.WriteAsync($"You are unauthorized to access the requested resource.");
					}
					else
					{
						await context.Response.WriteAsync($"Internal server error.");

					}
					await context.Response.WriteAsync($"</body></html>");

					context.Response.StatusCode = code;
				}));
		}
	}
}
