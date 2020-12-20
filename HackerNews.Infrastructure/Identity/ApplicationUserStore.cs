using HackerNews.Domain.Entities;
using HackerNews.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

namespace HackerNews.Infrastructure.Identity
{
	public class ApplicationUserStore : UserStore<User, IdentityRole<int>, HackerNewsContext, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityUserToken<int>, IdentityRoleClaim<int>>
	{
		public ApplicationUserStore(HackerNewsContext context, IdentityErrorDescriber describer = null) : base(context, describer)
		{
		}

		protected override IdentityUserClaim<int> CreateUserClaim(User user, Claim claim)
		{
			var userClaim = new IdentityUserClaim<int> { UserId = user.Id };
			userClaim.InitializeFromClaim(claim);
			return userClaim;
		}

		protected override IdentityUserLogin<int> CreateUserLogin(User user, UserLoginInfo login)
		{
			return new IdentityUserLogin<int>
			{
				UserId = user.Id,
				ProviderKey = login.ProviderKey,
				LoginProvider = login.LoginProvider,
				ProviderDisplayName = login.ProviderDisplayName
			};
		}

		protected override IdentityUserRole<int> CreateUserRole(User user, IdentityRole<int> role)
		{
			return new IdentityUserRole<int>
			{
				UserId = user.Id,
				RoleId = role.Id
			};
		}

		protected override IdentityUserToken<int> CreateUserToken(User user, string loginProvider, string name, string value)
		{
			return new IdentityUserToken<int>
			{
				UserId = user.Id,
				LoginProvider = loginProvider,
				Name = name,
				Value = value
			};
		}
	}
}
