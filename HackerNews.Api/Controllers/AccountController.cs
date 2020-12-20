﻿using AutoMapper;
using HackerNews.Api.Configuration;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Users.Commands.RegisterUser;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Controllers
{
	[Route("[controller]/[action]")]
	public class AccountController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IJwtGeneratorService _jwtGeneratorService;
		private readonly JwtSettings _jwtSettings;

		public AccountController(IMapper mapper,
			UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JwtSettings> jwtSettings, IJwtGeneratorService jwtGeneratorService)
		{
			_mapper = mapper;
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtGeneratorService = jwtGeneratorService;
			_jwtSettings = jwtSettings.Value;
		}

		[HttpPost]
		public async Task<Jwt> Login([FromBody] LoginModel loginModel)
		{
			var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, isPersistent: false, false);

			if (result.Succeeded)
			{
				var user = _userManager.Users.SingleOrDefault(u => u.UserName == loginModel.UserName);
				return await _jwtGeneratorService.GenererateJwtFromUser(user);
			}
			throw new NotFoundException();
		}

		[HttpPost]
		public async Task<Jwt> Register([FromBody] RegisterUserModel registerModel)
		{
			// TODO: most mediator commands and queries should return the actual entity; another command or helper should convert to models
			User user = await Mediator.Send(new RegisterUserCommand(registerModel));
			// should use UserAuthService. => should be in web project
			await _signInManager.SignInAsync(user, isPersistent: false);
			return await _jwtGeneratorService.GenererateJwtFromUser(user);
		}
	}
}
