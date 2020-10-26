using AutoMapper;
using FluentValidation;
using HackerNews.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HackerNews.Application
{
	public static class DepenedencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();
			services.AddAutoMapper(assembly);

			services.AddMediatR(assembly);

			// Adds validators to DI
			services.AddValidatorsFromAssembly(assembly);

			// Add validation behavior
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

			return services;
		}
	}
}
