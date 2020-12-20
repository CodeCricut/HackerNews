using HackerNews.Api.Configuration;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Web.Services
{
	class JwtGeneratorService : IJwtGeneratorService
	{
		private readonly IMediator _mediator;
		private JwtSettings _jwtSettings;

		public JwtGeneratorService(IOptions<JwtSettings> options, IMediator mediator)
		{
			_jwtSettings = options.Value;
			_mediator = mediator;
		}

		public async Task<Jwt> GenererateJwtFromUser(User user)
		{
			return await Task.Factory.StartNew(() =>
			{
				var claims = new List<Claim>
				{
					new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
				};

				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
				var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
				var expires = DateTime.Now.AddDays(Convert.ToDouble(7));

				var securityToken = new JwtSecurityToken(
					_jwtSettings.Issuer,
					_jwtSettings.Issuer,
					claims,
					expires: expires,
					signingCredentials: creds
				);

				string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
				Jwt token = new Jwt(securityToken.ValidTo, tokenString);
				return token;
			});
		}
	}
}
