using HackerNews.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

namespace HackerNews.Infrastructure.Identity
{
	public class ApplicationRoleStore : RoleStore<IdentityRole<int>, HackerNewsContext, int, IdentityUserRole<int>, IdentityRoleClaim<int>>
	{
		public ApplicationRoleStore(HackerNewsContext context, IdentityErrorDescriber describer = null) : base(context, describer)
		{
		}

		protected override IdentityRoleClaim<int> CreateRoleClaim(IdentityRole<int> role, Claim claim)
		{
			return new IdentityRoleClaim<int>
			{
				RoleId = role.Id,
				ClaimType = claim.Type,
				ClaimValue = claim.Value
			};
		}
	}
}
