﻿using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Users.Queries
{
	public class GetAuthenticatedUserTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldGetAuthenticatedUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var board = (await unitOfWork.Boards.GetEntitiesAsync()).First();
			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			var comment = (await unitOfWork.Comments.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var sut = new GetAuthenticatedUserHandler(userManager, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetPrivateUserModel sutResult = await sut.Handle(new GetAuthenticatedUserQuery(), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			Assert.Equal(user.Id, sutResult.Id);
		}
	}
}
