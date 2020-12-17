using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Common.DeletedEntityValidators;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Users.Queries.GetPublicUser;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Users.Queries.GetPublicUser
{
	public class GetPublicUserTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldGetPublicUser()
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

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			var deletedPolicyValidator = new Mock<DeletedUserPolicyValidator>();
			deletedPolicyValidator.Setup(pv => pv.ValidateEntity(It.IsAny<User>(), It.IsAny<DeletedEntityPolicy>()));

			var sut = new GetPublicUserHandler(deletedPolicyValidator.Object, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetPublicUserModel sutResult = await sut.Handle(new GetPublicUserQuery(user.Id), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			Assert.Equal(user.Id, sutResult.Id);
		}
	}
}
