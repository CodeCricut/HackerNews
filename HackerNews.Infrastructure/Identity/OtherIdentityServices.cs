using HackerNews.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace HackerNews.Infrastructure.Identity
{
	public class ApplicationRoleManager : RoleManager<IdentityRole<int>>
	{
		public ApplicationRoleManager(IRoleStore<IdentityRole<int>> store, IEnumerable<IRoleValidator<IdentityRole<int>>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<IdentityRole<int>>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
		{
		}
	}

	public class ApplicationSignInManager : SignInManager<User>
	{
		public ApplicationSignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<User> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
		{
		}
	}

	public class ApplicationUserManager : UserManager<User>
	{
		public ApplicationUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
		{
		}
	}
}
