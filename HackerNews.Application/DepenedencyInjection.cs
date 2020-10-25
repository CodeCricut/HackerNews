using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HackerNews.Application
{
	public static class DepenedencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();
			services.AddAutoMapper(assembly);
			services.AddValidatorsFromAssembly(assembly);
			services.AddMediatR(assembly);
			// Add custom pipeline behavior.

			return services;
		}
	}
}
