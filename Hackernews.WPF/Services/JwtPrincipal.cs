using HackerNews.Domain.Common.Models;

namespace Hackernews.WPF.Services
{
	public interface IJwtPrincipal
	{
		Jwt Jwt { get; }
		void SetJwt(Jwt jwt);
	}

	public class JwtPrincipal : IJwtPrincipal
	{
		public Jwt Jwt { get; private set; }

		public void SetJwt(Jwt jwt)
		{
			if (Jwt != jwt)
			{
				Jwt = jwt;
			}
		}
	}
}
