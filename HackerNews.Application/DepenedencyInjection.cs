﻿using FluentValidation;
using HackerNews.Application.Common.AdminLevelOperationValidators;
using HackerNews.Application.Common.Behaviors;
using HackerNews.Application.Common.DeletedEntityValidators;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HackerNews.Application
{
	public static class DepenedencyInjection
	{
		/// <summary>
		/// Register necessary CQRS services to the container.
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddMediatR(assembly);

			// Adds validators to DI
			services.AddValidatorsFromAssembly(assembly);

			// Add validation behavior
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

			// Add deleted entity policy validators
			services.AddSingleton<IDeletedEntityPolicyValidator<Article>, DeletedArticlePolicyValidator>();
			services.AddSingleton<IDeletedEntityPolicyValidator<Comment>, DeletedCommentPolicyValidator>();
			services.AddSingleton<IDeletedEntityPolicyValidator<Board>, DeletedBoardPolicyValidator>();
			services.AddSingleton<IDeletedEntityPolicyValidator<User>, DeletedUserPolicyValidator>();

			services.AddSingleton<IAdminLevelOperationValidator<Article>, AdminLevelArticleOperationValidator>();
			services.AddSingleton<IAdminLevelOperationValidator<Comment>, AdminLevelCommentOperationValidator>();

			return services;
		}
	}
}
