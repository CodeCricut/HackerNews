using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Common.DeletedEntityValidators;
using HackerNews.Application.Common.Interfaces;
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

namespace Application.IntegrationTests.Users.Queries.GetUserFromLoginModel
{
	public class GetUserFromLoginModelTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldGetUserFromLoginModel()
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

			var loginModel = new LoginModel
			{
				Username = user.UserName,
				Password = user.Password
			};

			var deletedPolicyValidator = new Mock<DeletedUserPolicyValidator>();
			deletedPolicyValidator.Setup(pv => pv.ValidateEntity(It.IsAny<User>(), It.IsAny<DeletedEntityPolicy>()));

			var sut = new GetUserFromLoginModelQueryHandler(deletedPolicyValidator.Object, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetPrivateUserModel sutResult = await sut.Handle(
				new GetUserFromLoginModelQuery(loginModel), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			Assert.Equal(user.UserName, sutResult.UserName);
			Assert.Equal(user.Id, sutResult.Id);
		}
	}
}
