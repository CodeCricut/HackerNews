using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Users.Commands.RegisterUser;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq;
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

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			//var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var board = (await unitOfWork.Boards.GetEntitiesAsync()).First();
			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			var comment = (await unitOfWork.Comments.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			//currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var registerUserModel = new RegisterUserModel
			{
				FirstName = "first name",
				LastName = "last name",
				Password = "unique password",
				UserName = Guid.NewGuid().ToString()
			};

			var sut = new RegisterUserHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetPrivateUserModel sutResult = await sut.Handle(new RegisterUserCommand(registerUserModel), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			var addedUser = await unitOfWork.Users.GetEntityAsync(sutResult.Id);
			Assert.NotNull(addedUser);

			Assert.Equal(registerUserModel.FirstName, addedUser.FirstName);
			Assert.Equal(registerUserModel.LastName, addedUser.LastName);
			Assert.Equal(registerUserModel.UserName, addedUser.UserName);
			Assert.Equal(registerUserModel.Password, addedUser.Password);

			Assert.Equal(registerUserModel.FirstName, sutResult.FirstName);
			Assert.Equal(registerUserModel.LastName, sutResult.LastName);
			Assert.Equal(registerUserModel.UserName, sutResult.Username);
			Assert.Equal(registerUserModel.Password, sutResult.Password);
		}
	}
}
