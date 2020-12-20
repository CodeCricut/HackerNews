using Application.IntegrationTests.Common;
using HackerNews.Application.Users.Commands.RegisterUser;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Users.Commands.RegisterUser
{
	public class RegisterUserTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldRegisterUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var registerUserModel = new RegisterUserModel
			{
				FirstName = "first name",
				LastName = "last name",
				Password = "unique password",
				UserName = Guid.NewGuid().ToString()
			};


			var sut = new RegisterUserHandler(userManager, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			User sutResult = await sut.Handle(new RegisterUserCommand(registerUserModel), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			var addedUser = await unitOfWork.Users.GetEntityAsync(sutResult.Id);
			Assert.NotNull(addedUser);

			Assert.Equal(registerUserModel.FirstName, addedUser.FirstName);
			Assert.Equal(registerUserModel.LastName, addedUser.LastName);
			Assert.Equal(registerUserModel.UserName, addedUser.UserName);

			Assert.Equal(registerUserModel.FirstName, sutResult.FirstName);
			Assert.Equal(registerUserModel.LastName, sutResult.LastName);
			Assert.Equal(registerUserModel.UserName, sutResult.UserName);
		}
	}
}
