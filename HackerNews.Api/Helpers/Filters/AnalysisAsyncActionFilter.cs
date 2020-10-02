using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.Filters
{
	/*
	 * https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-3.1
	  A filter can be added to the pipeline at one of three scopes:
			Using an attribute on a controller action. Filter attributes cannot be applied to Razor Pages handler methods.
			Using an attribute on a controller or Razor Page.
			Globally for all controllers, actions, and Razor Pages. Add filter in ConfigureServices
	 */
	public class AnalysisAsyncActionFilter : IAsyncActionFilter
	{
		private readonly ILogger<AnalysisAsyncActionFilter> _logger;

		public AnalysisAsyncActionFilter(ILogger<AnalysisAsyncActionFilter> logger)
		{
			_logger = logger;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			// Do something before the action executes.
			var controller = context.Controller.ToString();
			var action = context.ActionDescriptor.DisplayName;

			var watch = Stopwatch.StartNew();

			// next() calls the action method.
			var resultContext = await next();
			// resultContext.Result is set.
			// Do something after the action executes.
			watch.Stop();
			_logger.LogInformation($"Ran {controller}.{action} in {watch.Elapsed.TotalSeconds} seconds.");
		}
	}
}
