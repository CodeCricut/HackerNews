using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Users.Queries.GetUserByUsername;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Users.Queries.GetUserByUsername
{
	public class GetUserByUsernameTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldGetValidUser()
		{
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var board = (await unitOfWork.Boards.GetEntitiesAsync()).First();
			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			var comment = (await unitOfWork.Comments.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var sut = new GetUserByUsernameHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetPublicUserModel sutResult = await sut.Handle(
				new GetUserByUsernameQuery(user.Username), new System.Threading.CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			Assert.Equal(user.Username, sutResult.Username);
			Assert.Equal(user.Id, sutResult.Id);
		}
	}
}
