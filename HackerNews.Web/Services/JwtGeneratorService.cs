using HackerNews.Api.Configuration;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
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


		// TODO: this should really just generate the JWT from a user, seeing as the user could be retrieved using GetUserFromLoginModelQuery
		public async Task<Jwt> GenererateJwtFromUser(User user)
		{
			return await Task.Factory.StartNew(() =>
			{
				// generate token that is valid for 7 days
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
					Expires = DateTime.UtcNow.AddDays(7),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				};
				SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
				string tokenString = tokenHandler.WriteToken(securityToken);
				Jwt token = new Jwt(securityToken.ValidTo, tokenString);
				return token;
			});
		}
	}
}
