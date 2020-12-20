using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using HackerNews.EF;
using HackerNews.Infrastructure.Identity;
using HackerNews.Infrastructure.Repository.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HackerNews.Infrastructure
{
	public static class DependencyInjection
	{
		/// <summary>
		/// Add the required DB services to the container.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			if (configuration.GetValue<bool>("UseInMemoryDatabase"))
			{
				// In order to run tests in parallel without issues, DB name must be unique.
				var inMemoryDbName = $"HackerNewsDb{Guid.NewGuid()}";

				services.AddDbContext<DbContext, HackerNewsContext>(options =>
					options.UseInMemoryDatabase(inMemoryDbName)); ;
			}
			else
			{
				services.AddDbContext<DbContext, HackerNewsContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("HackerNews")
				));

				// by god i spent ages tracking down this bug. Identity needs the explicit HackerNewsContext service. the above registration was not sufficient
				services.AddDbContext<HackerNewsContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("HackerNews")
				));
			}

			// redirect entity services to custom
			services.AddScoped<
				UserStore<User, IdentityRole<int>, HackerNewsContext, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityUserToken<int>, IdentityRoleClaim<int>>,
				ApplicationUserStore>();
			services.AddScoped<UserManager<User>, ApplicationUserManager>();
			services.AddScoped<RoleManager<IdentityRole<int>>, ApplicationRoleManager>();
			services.AddScoped<SignInManager<User>, ApplicationSignInManager>();
			services.AddScoped<
				RoleStore<IdentityRole<int>, HackerNewsContext, int, IdentityUserRole<int>, IdentityRoleClaim<int>>,
				ApplicationRoleStore>();
			//services.AddScoped<IEmailSender, AuthMessageSender>();
			//services.AddScoped<ISmsSender, AuthMessageSender>();

			services.AddIdentity<User, IdentityRole<int>>(opt =>
			{
				opt.Password.RequireDigit = false;
				opt.Password.RequiredLength = 3;
				opt.Password.RequiredUniqueChars = 3;
				opt.Password.RequireLowercase = false;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireUppercase = false;
			})
				.AddUserStore<ApplicationUserStore>()
				.AddUserManager<ApplicationUserManager>()
				.AddRoleStore<ApplicationRoleStore>()
				.AddRoleManager<ApplicationRoleManager>()
				.AddSignInManager<ApplicationSignInManager>()
				// You **cannot** use .AddEntityFrameworkStores() when you customize everything
				//.AddEntityFrameworkStores<ApplicationDbContext, int>()
				.AddDefaultTokenProviders();

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}
