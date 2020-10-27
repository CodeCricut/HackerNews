using HackerNews.Api.Configuration;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Common.Models;
using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Users.Queries.GetUserFromLoginModel;
using HackerNews.Domain.Exceptions;
using HackerNews.Domain.Interfaces;
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
		private readonly IUnitOfWork _unitOfWork;
		private JwtSettings _jwtSettings;

		public JwtGeneratorService(IOptions<JwtSettings> options, IMediator mediator, IUnitOfWork unitOfWork)
		{
			_jwtSettings = options.Value;
			_mediator = mediator;
			_unitOfWork = unitOfWork;
		}


		public async Task<Jwt> GenererateJwtFromLoginModelAsync(LoginModel loginModel)
		{
			var user = await _mediator.Send(new GetUserFromLoginModelQuery(loginModel));
			if (user == null) throw new NotFoundException();

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
		}
	}
}
