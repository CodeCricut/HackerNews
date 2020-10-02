using HackerNews.Domain.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HackerNews.Api.Helpers.Middleware
{
	public class DeveloperMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<DeveloperMiddleware> _logger;

		public DeveloperMiddleware(RequestDelegate next, ILogger<DeveloperMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Path.StartsWithSegments("/developer"))
			{
				await context.Response.WriteAsync("Hello from the developer middleware!");

				// Assemblies are units of code that combine collections of types and resources into executables or dynamic link libararies (.exe or .dll).
				_logger.LogInformation("Controllers:");

				var controllerTypes = Assembly.GetExecutingAssembly().GetTypes()
					.Where(t => typeof(ControllerBase).IsAssignableFrom(t));

				// Log controller names, actions, and action params
				foreach(Type t in controllerTypes)
				{
					_logger.LogInformation($"\t{t.Name}");

					// MethodInfo is a built in type similar to Type for methods
					var controllerActions = t.GetMethods().Where(methodInfo => methodInfo.GetCustomAttributes()
						.Any(attr => attr is HttpMethodAttribute || attr is EnableQueryAttribute));
					foreach(var controllerAction in controllerActions)
					{
						_logger.LogInformation($"\t\t{controllerAction.Name}");
						var arguments = controllerAction.GetParameters();
						foreach (var arg in arguments) _logger.LogInformation($"\t\t\t{arg.ParameterType} {arg.Name}");
					}
				}
			}
			else 
				await _next(context);
		}
	}
}
